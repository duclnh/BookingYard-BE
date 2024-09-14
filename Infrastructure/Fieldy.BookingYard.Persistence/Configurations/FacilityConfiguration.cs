using Fieldy.BookingYard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fieldy.BookingYard.Persistence.Configurations;

public class FacilityConfiguration : IEntityTypeConfiguration<Facility>
{
    public void Configure(EntityTypeBuilder<Facility> builder)
    {
        builder.Property(x => x.Id)
               .HasColumnName("FacilityID");
        builder.Property(x => x.Name)
               .HasColumnName("FacilityName");
        builder.Property(x => x.MonthPrice)
               .HasColumnType("decimal(18,2)");
        builder.Property(x => x.YearPrice)
               .HasColumnType("decimal(18,2)");
        builder.Property(x => x.PeakHourPrice)
               .HasColumnType("decimal(18,2)");
        builder.Property(x => x.HolidayPrice)
               .HasColumnType("decimal(18,2)");

        builder.HasData(
          new Facility
          {
              Id = Guid.Parse("E175DAF6-B5A4-4D0E-544D-08DCD4D409D4"),
              Name = "Sân Bóng Trường Cao Đẳng Công Nghệ Thủ Đức",
              Address = "53 Võ Văn Ngân",
              FullAddress = "53 Võ Văn Ngân, Phường Linh Chiểu, Thành Phố Thủ Đức, Thành phố Hồ Chí Minh",
              Latitude = 10.85155894385053,
              Longitude = 106.75803191349321,
              Description = $@"
                            <h1>Sân Bóng Trường Cao Đẳng Công Nghệ Thủ Đức</h1>
    
                            <p>
                                Sân Bóng Trường Cao Đẳng Công Nghệ Thủ Đức nằm tại quận Thủ Đức, TP.HCM, 
                                là một sân bóng mini với mặt cỏ nhân tạo chất lượng cao. Sân được thiết kế 
                                phù hợp cho các trận đấu 5 người, có hệ thống chiếu sáng hiện đại phục vụ 
                                các trận vào buổi tối.
                            </p>

                            <p>
                                Ngoài ra, sân còn có khu vực ghế ngồi cho khán giả, phòng thay đồ, và dịch vụ 
                                cho thuê bóng, giày. Đây là địa điểm lý tưởng cho học sinh, sinh viên và người 
                                dân khu vực đến luyện tập thể thao và tổ chức các giải đấu giao hữu.
                            </p>

                            <p>
                                Sân hoạt động từ sáng đến tối với chi phí hợp lý. Sân Bóng Trường Cao Đẳng Công 
                                Nghệ Thủ Đức không chỉ thu hút sinh viên của trường mà còn là địa điểm quen thuộc 
                                cho các đội bóng trong khu vực đến luyện tập và thi đấu.
                            </p>

                            <p>
                                Sân được bảo trì thường xuyên, đảm bảo mặt cỏ luôn trong tình trạng tốt. Khu vực 
                                xung quanh sân có các tiện ích như căng tin và chỗ để xe rộng rãi, tạo sự thuận tiện 
                                cho người chơi.
                            </p>

                            <p>
                                Ngoài ra, sân còn có dịch vụ đặt trước online, giúp người chơi dễ dàng sắp xếp lịch thi đấu. 
                                Đây là nơi thích hợp để giao lưu, rèn luyện sức khỏe và tổ chức các sự kiện thể thao.
                             </p>
                            ",
              ProvinceID = 79,
              DistrictID = 769,
              WardID = 26815,
              Image = "/facility/31fff3b5-663d-49af-b2b6-64fe034f1304.jpg",
              Logo = "/facility/d8009fa4-fd59-4784-85c7-dca24465e308.jpg",
              Convenient = @"
                             [
                              {
                                ""title"": ""payment"",
                                ""content"": ""Các phương thức thanh toán"",
                                ""icon"": ""MdPayments"",
                                ""feature"": [
                                  {
                                    ""title"": ""Thẻ tín dụng (Visa, Master card)"",
                                    ""isEnable"": true
                                  },
                                  {
                                    ""title"": ""Momo"",
                                    ""isEnable"": true
                                  },
                                  {
                                    ""title"": ""Chuyển khoản"",
                                    ""isEnable"": true
                                  },
                                  {
                                    ""title"": ""Tiền mặt"",
                                    ""isEnable"": true
                                  }
                                ]
                              },
                              {
                                ""title"": ""entertainment"",
                                ""content"": ""Các dịch vụ giải trí"",
                                ""icon"": ""IoStorefrontSharp"",
                                ""feature"": [
                                  {
                                    ""title"": ""Nhà hàng"",
                                    ""isEnable"": true
                                  },
                                  {
                                    ""title"": ""Quán nước"",
                                    ""isEnable"": true
                                  },
                                  {
                                    ""title"": ""Căn tin"",
                                    ""isEnable"": true
                                  },
                                  {
                                    ""title"": ""Karoke"",
                                    ""isEnable"": true
                                  },
                                  {
                                    ""title"": ""Cửa hàng tiện lợi"",
                                    ""isEnable"": true
                                  }
                                ]
                              },
                              {
                                ""title"": ""safe"",
                                ""content"": ""Các dịch vụ an toàn và chăm sóc"",
                                ""icon"": ""MdHealthAndSafety"",
                                ""feature"": [
                                  {
                                    ""title"": ""Bảo vệ"",
                                    ""isEnable"": true
                                  },
                                  {
                                    ""title"": ""Nhà gửi xe"",
                                    ""isEnable"": true
                                  },
                                  {
                                    ""title"": ""Chăm sóc y tế"",
                                    ""isEnable"": true
                                  }
                                ]
                              }
                            ]
                            ",
              StartTime = new TimeSpan(8, 0, 0),
              EndTime = new TimeSpan(24, 0, 0),
              HolidayPrice =0,
              MonthPrice= 0,
              PeakHourPrice=0,
              YearPrice=0,
              UserID = Guid.Parse("03b17c1c-0403-2e08-9ed3-709339dd911b"),
              CreatedAt = DateTime.Now,
              CreatedBy = Guid.Parse("03b17c1c-0403-4e08-9ed6-738939dd911b"),
              ModifiedAt = DateTime.Now,
              ModifiedBy = Guid.Parse("03b17c1c-0403-4e08-9ed6-738939dd911b"),
          }
        );

    }
}