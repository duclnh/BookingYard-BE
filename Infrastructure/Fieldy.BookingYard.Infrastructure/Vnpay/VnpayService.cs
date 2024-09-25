using Fieldy.BookingYard.Application.Abstractions.Vnpay;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Fieldy.BookingYard.Infrastructure.Vnpay
{
	public class VnpayService : IVnpayService
	{
		private SortedList<String, String> _requestData = new SortedList<String, String>(new VnPayCompare());
		private SortedList<String, String> _responseData = new SortedList<String, String>(new VnPayCompare());
		private static IHttpContextAccessor? _httpContextAccessor;
		public VnpayService(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public void AddRequestData(string key, string value)
		{
			if (!String.IsNullOrEmpty(value))
			{
				_requestData.Add(key, value);
			}
		}

		public void AddResponseData(string key, string value)
		{
			if (!String.IsNullOrEmpty(value))
			{
				_responseData.Add(key, value);
			}
		}

		public string GetResponseData(string key)
		{
			string retValue;
			if (_responseData.TryGetValue(key, out retValue))
			{
				return retValue;
			}
			else
			{
				return string.Empty;
			}
		}
		public string CreateRequestUrl(string baseUrl, string vnp_HashSecret)
		{
			StringBuilder data = new StringBuilder();
			foreach (KeyValuePair<string, string> kv in _requestData)
			{
				if (!String.IsNullOrEmpty(kv.Value))
				{
					data.Append(WebUtility.UrlEncode(kv.Key) + "=" + WebUtility.UrlEncode(kv.Value) + "&");
				}
			}
			string queryString = data.ToString();

			baseUrl += "?" + queryString;
			String signData = queryString;
			if (signData.Length > 0)
			{

				signData = signData.Remove(data.Length - 1, 1);
			}
			string vnp_SecureHash = HmacSHA512(vnp_HashSecret, signData);
			baseUrl += "vnp_SecureHash=" + vnp_SecureHash;

			return baseUrl;
		}

		public String HmacSHA512(string key, String inputData)
		{
			var hash = new StringBuilder();
			byte[] keyBytes = Encoding.UTF8.GetBytes(key);
			byte[] inputBytes = Encoding.UTF8.GetBytes(inputData);
			using (var hmac = new HMACSHA512(keyBytes))
			{
				byte[] hashValue = hmac.ComputeHash(inputBytes);
				foreach (var theByte in hashValue)
				{
					hash.Append(theByte.ToString("x2"));
				}
			}

			return hash.ToString();
		}
		public string GetIpAddress()
		{
			string? ipAddress;
			try
			{
				ipAddress = _httpContextAccessor.HttpContext?.Connection?.LocalIpAddress?.ToString();
			}
			catch (Exception ex)
			{
				ipAddress = "Invalid IP:" + ex.Message;
			}

			return ipAddress;
		}
	}
	/*public class VnpayUtils
	{
		private static IHttpContextAccessor? _httpContextAccessor;
		public VnpayUtils(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public static String HmacSHA512(string key, String inputData)
		{
			var hash = new StringBuilder();
			byte[] keyBytes = Encoding.UTF8.GetBytes(key);
			byte[] inputBytes = Encoding.UTF8.GetBytes(inputData);
			using (var hmac = new HMACSHA512(keyBytes))
			{
				byte[] hashValue = hmac.ComputeHash(inputBytes);
				foreach (var theByte in hashValue)
				{
					hash.Append(theByte.ToString("x2"));
				}
			}

			return hash.ToString();
		}
		public static string GetIpAddress()
		{
			string? ipAddress;
			try
			{
				ipAddress = _httpContextAccessor.HttpContext?.Connection?.LocalIpAddress?.ToString();
			}
			catch (Exception ex)
			{
				ipAddress = "Invalid IP:" + ex.Message;
			}

			return ipAddress;
		}
	}*/
	public class VnPayCompare : IComparer<string>
	{
		public int Compare(string x, string y)
		{
			if (x == y) return 0;
			if (x == null) return -1;
			if (y == null) return 1;
			var vnpCompare = CompareInfo.GetCompareInfo("en-US");
			return vnpCompare.Compare(x, y, CompareOptions.Ordinal);
		}
	}
}
