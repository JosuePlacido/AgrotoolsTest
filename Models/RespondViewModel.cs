using System.ComponentModel.DataAnnotations;

namespace Agrotools.Models
{
	public class RespondViewModel
	{
		[Required]
		public float[] Lat { get; set; }
		[Required]
		public float[] Long { get; set; }
		[Required]

		public string[] Answer { get; set; }
		[Required]
		public string[] QuestionId { get; set; }
		public string Quiz { get; set; }



	}
}
