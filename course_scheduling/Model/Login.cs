using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace course_scheduling.Model
{
    public class Login
    {
        private crschContext context;

        public String User { get; set; }

        public string Password { get; set; }

        public string type { get; set; }

        
    }
}