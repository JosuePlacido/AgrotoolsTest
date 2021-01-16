using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Agrotools.Models
{
	[Table("tb_answer")]
	public class Answer : Entity
	{
		[Display(Name = "Resposta")]
		[Required(ErrorMessage = "Campo obrigatório")]
		public string Text { get; set; }
		[DataType(DataType.Date)]
		[Display(Name = "Data")]
		public DateTime Date { get; set; }
		[Required(ErrorMessage = "Campo obrigatório")]
		[Display(Name = "Latitude")]
		public float Lat { get; set; }
		[Required(ErrorMessage = "Campo obrigatório")]
		[Display(Name = "Longitude")]
		public float Long { get; set; }
		public string QuestionId { get; set; }



		public override string ToString()
		{
			return Text.Substring(0, 50);
		}
	}
}
