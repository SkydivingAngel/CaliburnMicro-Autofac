using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    using System.Windows;

    public interface IRepo
    {
        string Nome { get; set; }
        List<string> AllLines { get; set; }
    }

    public class Repo : IRepo
    {
        public string Nome { get; set; }

        public List<string> AllLines { get; set; }

        //public Repo()
        //{

        //}

        public Repo(string nome)
        {
            Nome = nome;
            //MessageBox.Show("instance");
        }
    }
}
