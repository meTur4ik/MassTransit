namespace MassTransit.ServiceBus.Tests.JsonPlay
{
    using Newtonsoft.Json;
    using NUnit.Framework;
    using NUnit.Framework.SyntaxHelpers;

    [TestFixture]
    public class As_a_Json_Serializer
    {
        [Test]
        public void NAME()
        {
            string json = JavaScriptConvert.SerializeObject(new Bob("Chris"));

            Bob clone = JavaScriptConvert.DeserializeObject<Bob>(json);
            Assert.That(clone.Friend, Is.EqualTo("Chris"));
        }
    }


    public class Bob : IMessage
    {
        private string _friend;

        //for JSON
        public Bob()
        {
        }

        public Bob(string friend)
        {
            _friend = friend;
        }

        public string Friend
        {
            get { return _friend; }
            set { _friend = value; }
        }
    }
}