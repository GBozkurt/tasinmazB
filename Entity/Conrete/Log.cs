using System.ComponentModel.DataAnnotations;
using System;

namespace tasinmazz.Entity.Conrete
{
	public class Log
	{
		public int Id { get; set; }

		[Required]
		public int UserId { get; set; }

		[Required]
		public string Durum { get; set; }

		[Required]
		[MaxLength(50)]
		public string IslemTip { get; set; }

		[Required]
		[MaxLength(500)]
		public string Aciklama { get; set; }

		[Required]
		public DateTime TarihSaat { get; set; }
		[Required]
		public string KullaniciIp { get; set; }

	}
}
