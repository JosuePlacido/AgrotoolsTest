using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Agrotools.Models
{
	[Table("tb_quiz")]
	public class Quiz : Entity
	{
		[Display(Name = "Nome")]
		[Required(ErrorMessage = "Campo obrigatório")]
		public string Title { get; set; }
		[Display(Name = "Descrição")]
		public string Description { get; set; }
		[Display(Name = "Criado em")]
		[DataType(DataType.Date)]
		public DateTime CreatedAt { get; set; }
		public string OwnerId { get; set; }
		[Display(Name = "Autor")]
		public User Owner { get; set; }
		[Display(Name = "Perguntas")]
		public IList<Question> Questions { get; set; }
		public override string ToString()
		{
			return Title.Substring(0, 50);
		}
	}
}
