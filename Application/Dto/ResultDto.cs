using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class ResultDto<T>
    {
        public int CodeError { get; set; }
        public string Msj { get; set; }
        public T? Data { get; set; }
    }
}
