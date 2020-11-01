using System;
using System.Reflection;
using Disassembler.Entity;
using Disassembler.Service;
using NUnit.Framework;

namespace DisassemblerTest
{
    class Foo
    {
        public static Foo CreateFromFuncs<T1, T2>(Func<T1, T2> f1, Func<T2, T1> f2)
        {
            return null;
        }
    }

    public static class MyExtensions
    {
        public static int WordCount(this string str)
        {
            return str.Split(new char[] {' ', '.', '?'},
                StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public static int Asd()
        {
            return 0;
        }
    }

    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void SetUp()
        {
            IDisassembler disassembler = new Disassembler.Service.Impl.Disassembler();
            var assembly = Assembly.LoadFrom("DisassemblerTest.dll");
            _actual = disassembler.Disassemble(assembly);
        }

        private AssemblyInfo _actual;

        [Test]
        public void AssemblyTest()
        {
            Assert.IsNotNull(_actual);
        }

        [Test]
        public void NamespaceTest()
        {
            var namespaceInfos = _actual.Namespaces;
            CollectionAssert.AllItemsAreNotNull(namespaceInfos);
            CollectionAssert.AllItemsAreUnique(namespaceInfos);
        }

        [Test]
        public void ClassTest()
        {
            var namespaceInfos = _actual.Namespaces;
            foreach (var namespaceInfo in namespaceInfos)
            {
                var classInfos = namespaceInfo.Classes;
                CollectionAssert.AllItemsAreNotNull(classInfos);
                CollectionAssert.AllItemsAreUnique(classInfos);
            }
        }

        [Test]
        public void MemberTest()
        {
            var namespaceInfos = _actual.Namespaces;
            foreach (var namespaceInfo in namespaceInfos)
            {
                var classInfos = namespaceInfo.Classes;
                foreach (var classInfo in classInfos)
                {
                    CollectionAssert.AllItemsAreNotNull(classInfo.Fields);
                    CollectionAssert.AllItemsAreUnique(classInfo.Fields);
                    CollectionAssert.AllItemsAreNotNull(classInfo.Properties);
                    CollectionAssert.AllItemsAreUnique(classInfo.Properties);
                    CollectionAssert.AllItemsAreNotNull(classInfo.Methods);
                    CollectionAssert.AllItemsAreUnique(classInfo.Methods);
                    CollectionAssert.AllItemsAreNotNull(classInfo.Constructors);
                    CollectionAssert.AllItemsAreUnique(classInfo.Constructors);
                }
            }
        }

        [Test]
        public void MemberValueTest()
        {
            Assert.AreEqual("System", _actual.Namespaces[1].Name);
            Assert.AreEqual("DisassemblerTest", _actual.Namespaces[0].Name);
            Assert.AreEqual("System", _actual.Namespaces[1].Name);
            Assert.AreEqual(typeof(Foo), _actual.Namespaces[0].Classes[0].ClassType);
            Assert.AreEqual(typeof(MyExtensions), _actual.Namespaces[0].Classes[1].ClassType);
            Assert.AreEqual("CreateFromFuncs", _actual.Namespaces[0].Classes[0].Methods[0].Info.Name);
        }
    }
}