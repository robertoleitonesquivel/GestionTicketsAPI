﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuniBarva.MODELS.DTO
{
    public class MenuDTO
    {
    public int Id { get; set; }
        public string Navigation { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public int IdMenuPadre { get; set; }
    }
}
