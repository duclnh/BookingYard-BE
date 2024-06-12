namespace Fieldy.BookingYard.Domain.Common
{
    public interface IAuditEntity {

        public DateTime? CreateDate { get; set; }
        public string? CreateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string? UpdateBy { get; set; }

    }
}


