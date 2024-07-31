using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace tasinmazz.Entity.Conrete
{
	public class Mahalle
	{
		public int Id { get; set; }
		[NotNull]
		public string MahalleAdi { get; set; }

		[ForeignKey("IlceId")]
		[NotNull]
		public int IlceId { get; set; }

		public virtual Ilce Ilce { get; set; }
	}
}
	