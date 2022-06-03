using Prody.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prody.BLL.DTOs
{
    public class ReadProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string Unit { get; set; }

        public ReadCategoryDto Category { get; set; }

        public List<ReadPriceDto> Prices { get; set; }
    }
}
