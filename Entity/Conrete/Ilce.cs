using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace tasinmazz.Entity.Conrete
{
	public class Ilce
	{
		public int Id { get; set; }
		[NotNull]
		public string IlceAdi { get; set; }
		[ForeignKey("IlId")]
		[NotNull]
		public int IlId { get; set; }
		public virtual Il Il { get; set; }
	}
}
