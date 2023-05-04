﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P013EStore.Core.Entities
{
	public class Category : IEntity
	{
        public int Id { get; set; }
		[Display(Name="Ad")]
		public string Name { get; set; }
		[Display(Name = "Açıklama")]
		public string? Description { get; set; }
		[Display(Name = "Resim")]
		public string? Image { get; set; }
		[Display(Name = "Durum")]
		public bool IsActive { get; set; }
		[Display(Name = "Üst Menüde Göster")]
		public bool IsTopMenu { get; set; }
		public int ParentId { get; set; }

	}
}
