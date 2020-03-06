namespace ConsoleApp1
{
    public class Carro
    {
        public string Modelo { set; get; }
        public string Marca { set; get; }

        public Carro DevolverCarro()
        {
            return new Carro();
        }
    }
}
