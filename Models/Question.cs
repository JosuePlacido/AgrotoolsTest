using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Agrotools.Models
{
	[Table("tb_question")]
	public class Question : Entity
	{
		[Display(Name = "Pergunta")]
		[Required(ErrorMessage = "Campo obrigatório")]
		public string Title { get; set; }
		public int Number { get; set; }


		public string QuizId { get; set; }
		public IList<Answer> Answers { get; set; }


		public override string ToString()
		{
			return Title.Substring(0, 50);
		}
	}
}
