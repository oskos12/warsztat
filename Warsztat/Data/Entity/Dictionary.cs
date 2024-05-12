﻿using System.ComponentModel.DataAnnotations;

namespace Warsztat.Data.Entity
{
    public class Dictionary
    {
        [Key]
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string? Description { get; set; }
    }
}
