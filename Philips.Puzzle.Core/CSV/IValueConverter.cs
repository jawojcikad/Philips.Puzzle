using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Philips.Puzzle.Core.CSV
{
    public interface IValueConverter
    {
        Type Type { get;}
        object Convert(string value);
    }
}
