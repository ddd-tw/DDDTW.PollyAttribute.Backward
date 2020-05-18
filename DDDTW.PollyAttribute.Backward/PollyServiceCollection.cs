using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using AspectCore.Extensions.DependencyInjection;
using DDDTW.PollyAttribute.Backward.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace DDDTW.PollyAttribute.Backward
{
    public class PollyServiceCollection : IServiceCollection
    {
        private readonly IServiceCollection _serviceCollection;

        #region Constructors

        public PollyServiceCollection()
        {
            _serviceCollection = new ServiceCollection();
        }

        public PollyServiceCollection(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
        }

        #endregion Constructors

        #region Properties

        public int Count => _serviceCollection.Count;

        public bool IsReadOnly { get; }

        public bool IsReadyOnly => _serviceCollection.IsReadOnly;

        #endregion Properties

        public ServiceDescriptor this[int index]
        {
            get => _serviceCollection[index];
            set => _serviceCollection[index] = value;
        }

        public void CompleteRegistration()
        {
            _serviceCollection.AddMvcControllers("*");

            var provider = _serviceCollection.ConfigureDynamicProxy();
            var defaultResolver = new DefaultDependencyResolver(provider.BuildServiceContextProvider());
            DependencyResolver.SetResolver(defaultResolver);
        }

        #region Implement IServiceCollection Interface

        public void Clear() => _serviceCollection.Clear();

        public bool Contains(ServiceDescriptor item) => _serviceCollection.Contains(item);

        public void CopyTo(ServiceDescriptor[] array, int arrayIndex) => _serviceCollection.CopyTo(array, arrayIndex);

        public bool Remove(ServiceDescriptor item) => _serviceCollection.Remove(item);

        public IEnumerator<ServiceDescriptor> GetEnumerator() => _serviceCollection.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add(ServiceDescriptor item) => _serviceCollection.Add(item);

        public int IndexOf(ServiceDescriptor item) => _serviceCollection.IndexOf(item);

        public void Insert(int index, ServiceDescriptor item) => _serviceCollection.Insert(index, item);

        public void Remove(int index) => _serviceCollection.RemoveAt(index);

        public void RemoveAt(int index)
            => _serviceCollection.RemoveAt(index);

        #endregion Implement IServiceCollection Interface
    }
}