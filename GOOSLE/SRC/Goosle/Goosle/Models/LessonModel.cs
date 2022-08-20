using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goosle.Models
{
    struct LessonModel
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public string Teacher { get; set; }
        public string StartedAt { get; set; }
        public string Office { get; set; }
        public string WeekType { get; set; }
    }
}
