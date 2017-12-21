// ReSharper disable InheritdocInvalidUsage

namespace GoodGameDeals.Collections.ObjectModel {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Runtime.Serialization;

    /// <inheritdoc cref="LinkedList{T}" />
    /// <summary>
    ///     Represents an observable doubly linked list.
    /// </summary>
    [Serializable]
    public class ObservableLinkedList<T> : ICollection<T>,
                                           ICollection,
                                           IReadOnlyCollection<T>,
                                           ISerializable,
                                           IDeserializationCallback,
                                           INotifyCollectionChanged {
        /// <summary>
        ///     The linked list.
        /// </summary>
        private readonly LinkedList<T> linkedList;

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="ObservableLinkedList{T}" /> class that is empty.
        /// </summary>
        public ObservableLinkedList() {
            this.linkedList = new LinkedList<T>();
        }

        /// <inheritdoc />
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        ///     Gets the first node of the <see cref="ObservableLinkedList{T}" />.
        /// </summary>
        public LinkedListNode<T> First => this.linkedList.First;

        /// <inheritdoc />
        public bool IsReadOnly => ((ICollection<T>)this.linkedList).IsReadOnly;

        /// <inheritdoc />
        public bool IsSynchronized =>
            ((ICollection)this.linkedList).IsSynchronized;

        /// <summary>
        ///     Gets the last node of the <see cref="ObservableLinkedList{T}" />.
        /// </summary>
        public LinkedListNode<T> Last => this.linkedList.Last;

        /// <inheritdoc />
        public object SyncRoot => ((ICollection)this.linkedList).SyncRoot;

        /// <inheritdoc />
        int ICollection.Count => this.linkedList.Count;

        /// <inheritdoc />
        int ICollection<T>.Count => this.linkedList.Count;

        /// <inheritdoc />
        int IReadOnlyCollection<T>.Count => this.linkedList.Count;

        /// <inheritdoc />
        public void Add(T item) => ((ICollection<T>)this.linkedList).Add(item);

        /// <summary>
        ///     Adds a new node containing the specified value after the
        ///     specified existing node in the
        ///     <see cref="ObservableLinkedList{T}" />.
        /// </summary>
        /// <param name="node">
        ///     The <see cref="LinkedListNode{T}" /> after which to insert a
        ///     new <see cref="LinkedListNode{T}" /> containing <code>value</code>.
        /// </param>
        /// <param name="value">
        ///     The value to add to the <see cref="LinkedList{T}"/>.
        /// </param>
        /// <returns>
        ///     The new <see cref="LinkedListNode{T}"/> containing <code>value</code>
        /// </returns>
        public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T value) =>
            this.linkedList.AddAfter(node, value);

        /// <inheritdoc />
        public void Clear() => this.linkedList.Clear();

        /// <inheritdoc />
        public bool Contains(T item) => this.linkedList.Contains(item);

        /// <inheritdoc />
        public void CopyTo(T[] array, int arrayIndex) =>
            this.linkedList.CopyTo(array, arrayIndex);

        /// <inheritdoc />
        public void CopyTo(Array array, int index) =>
            ((ICollection)this.linkedList).CopyTo(array, index);

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator() =>
            this.linkedList.GetEnumerator();

        /// <inheritdoc />
        public void GetObjectData(
            SerializationInfo info,
            StreamingContext context) =>
            this.linkedList.GetObjectData(info, context);

        /// <inheritdoc />
        public void OnDeserialization(object sender) =>
            this.linkedList.OnDeserialization(sender);

        /// <inheritdoc />
        public bool Remove(T item) => this.linkedList.Remove(item);

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}