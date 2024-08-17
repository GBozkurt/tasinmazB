using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace tasinmazz.Entity.Conrete
{
	public class Tasinmaz
	{
		public int? Id { get; set; }

		[Required]
		[ForeignKey("MahalleId")]
		public int? MahalleId { get; set; }
		[AllowNull]
		public virtual Mahalle Mahalle { get; set; }
		[Required]
		public string Ada { get; set; }
		[Required]
		public string Parsel { get; set; }
		[Required]
		public string Nitelik { get; set; }
		[Required]
		public string KoordinatBilgileri { get; set; }
		[Required]
		[ForeignKey("UserId")]
		public int? UserId { get; set; }
		[AllowNull]
		public virtual User User { get; set; }
	}
}
