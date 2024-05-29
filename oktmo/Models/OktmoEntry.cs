using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace oktmo.Models
{
	public class OktmoEntry
	{
		[Key]
		public int Id { get; set; }

		[NotMapped]
		[StringLength(2)]
		public string Ter => Octmo[..2];

        [NotMapped]
		public string Kod1 => Octmo[2..5];

		[NotMapped]
		public string Kod2 => Octmo[5..8];

        [NotMapped]
		public string Kod3 => Octmo[8..];

		[Required]
        [StringLength(11)]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Код ОКТМО должен содержать 11 цифр.")]
        [DisplayName("Код ОКТМО")]
        public string Octmo { get; set; }

		[Required]
		[StringLength(1)]
        [DisplayName("Раздел")]
        [RegularExpression(@"^\d{1}$", ErrorMessage = "Раздел должен содержать 1 цифру.")]
		public string Razdel { get; set; }

		[StringLength(500)]
        [DisplayName("Наименование населенного пункта")]

        public string Name { get; set; }

		[Required]
		public bool IsActual { get; set; } = true;

	}
}
