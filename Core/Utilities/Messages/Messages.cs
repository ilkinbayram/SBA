namespace Core.Utilities.Messages
{
    public class Messages
    {
        public class AdvisorGuessMessages
        {
            private string[] _constantMessages = new string[] 
            {
                "Maç tahminleri her zaman garanti olmadığından, bir maça yüksek miktarda para yatırmaktan kaçının. Ben sadece analizlere bakarak size şahsi fikrimi bildirmekle yükümlüyüm. Bu yüzden tüm sorumluluk sizde.",
                "Maç tahminlerine bahis yaparken, kaybetmeyi göze alabileceğiniz miktarlarla bahis yapın. Futbolda hiçbir maç garanti değildir.",
                "Yine de futbolda herşeyin olabileceğini göz onünde bulundurun ve %99 tahminde bulunduğunuz maçlarda bile sadece eğlence için küçük miktarda bahisler yapın ve paranızı riske atmayın."
            };

            private Random _random = new Random();

            public string Message
            {
                get => _constantMessages[_random.Next(0, _constantMessages.Length)];
            }
        }
    }
}
