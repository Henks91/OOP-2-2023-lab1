//using System;
//using System.Collections.Generic;
//using Entiteter;

//namespace Datalager
//{
//    public class UnitOfWork
//    {
//        /// <summary>
//        ///  This class is used to access the storage in the application.
//        /// </summary>
//        public class UnitOfWork
//        {
//            public Repository<Foo> FooRepository
//            {
//                get; private set;
//            }
//            public Repository<Bar> BarRepository
//            {
//                get; private set;
//            }
//            /// <summary>
//            ///  Create a new instance.
//            /// </summary>
//            public UnitOfWork()
//            {
//                FooRepository = new Repository<Foo>();
//                BarRepository = new Repository<Bar>();
//                // Initialize the tables if this is the first UnitOfWork.
//                if (FooRepository.IsEmpty())
//                {
//                    Fill();
//                }
//            }
//            /// <summary>
//            ///  Save the changes made. Does nothing in this case.
//            /// </summary>
//            public void Save()
//            { }
//            private void Fill()
//            {
//                FooRepository.Add(new Foo(/* ... */));
//                FooRepository.Add(new Foo(/* ... */));
//                FooRepository.Add(new Foo(/* ... */));
//                FooRepository.Add(new Foo(/* ... */));
//                BarRepository.Add(new Bar(/* ... */));
//                BarRepository.Add(new Bar(/* ... */));
//            }
//        }
//    }
//}