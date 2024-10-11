using System.Collections;
using System.Collections.Generic;

public class LinkedList : IEnumerable<int>
{
    private Node? _head;
    private Node? _tail;

    /// <summary>
    /// Insert a new node at the front (i.e., the head) of the linked list.
    /// </summary>
    public void InsertHead(int value)
    {
        // Create new node
        Node newNode = new(value);
        // If the list is empty, then point both head and tail to the new node.
        if (_head is null)
        {
            _head = newNode;
            _tail = newNode;
        }
        // If the list is not empty, then only head will be affected.
        else
        {
            newNode.Next = _head; // Connect new node to the previous head
            _head.Prev = newNode; // Connect the previous head to the new node
            _head = newNode; // Update the head to point to the new node
        }
    }

    /// <summary>
    /// Insert a new node at the back (i.e., the tail) of the linked list.
    /// </summary>
    public void InsertTail(int value)
    {
        // Problem 1: Implement InsertTail
        Node newNode = new(value); // Crear un nuevo nodo con el valor proporcionado

        if (_tail is null)
        {
            // Si la lista está vacía, el nuevo nodo será tanto la cabeza como la cola
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            newNode.Prev = _tail; // Conectar el nuevo nodo al nodo actual de la cola
            _tail.Next = newNode; // Conectar el nodo actual de la cola al nuevo nodo
            _tail = newNode; // Actualizar la cola para que apunte al nuevo nodo
        }
    }

    /// <summary>
    /// Remove the first node (i.e., the head) of the linked list.
    /// </summary>
    public void RemoveHead()
    {
        // If the list has only one item in it, then set head and tail
        // to null resulting in an empty list.  This condition will also
        // cover an empty list.  It's okay to set to null again.
        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        // If the list has more than one item in it, then only the head
        // will be affected.
        else if (_head is not null)
        {
            _head.Next!.Prev = null; // Disconnect the second node from the first node
            _head = _head.Next; // Update the head to point to the second node
        }
    }

    /// <summary>
    /// Remove the last node (i.e., the tail) of the linked list.
    /// </summary>
    public void RemoveTail()
    {
        // Problem 2: Implement RemoveTail
        if (_tail is null)
        {
            // Si la lista está vacía, no hay nada que remover
            return;
        }

        if (_head == _tail)
        {
            // Si solo hay un nodo en la lista, removerlo y establecer head y tail a null
            _head = null;
            _tail = null;
        }
        else
        {
            // Desconectar el nodo actual de la cola
            _tail = _tail.Prev; // Actualizar la cola para que apunte al penúltimo nodo
            if (_tail != null)
            {
                _tail.Next = null; // El nuevo tail no tiene siguiente nodo
            }
        }
    }

    /// <summary>
    /// Insert 'newValue' after the first occurrence of 'value' in the linked list.
    /// </summary>
    public void InsertAfter(int value, int newValue)
    {
        // Search for the node that matches 'value' by starting at the
        // head of the list.
        Node? curr = _head;
        while (curr is not null)
        {
            if (curr.Data == value)
            {
                // If the location of 'value' is at the end of the list,
                // then we can call insert_tail to add 'newValue'
                if (curr == _tail)
                {
                    InsertTail(newValue);
                }
                // For any other location of 'value', need to create a
                // new node and reconnect the links to insert.
                else
                {
                    Node newNode = new(newValue);
                    newNode.Prev = curr; // Connect new node to the node containing 'value'
                    newNode.Next = curr.Next; // Connect new node to the node after 'value'
                    curr.Next!.Prev = newNode; // Connect node after 'value' to the new node
                    curr.Next = newNode; // Connect the node containing 'value' to the new node
                }

                return; // We can exit the function after we insert
            }

            curr = curr.Next; // Go to the next node to search for 'value'
        }
    }

    /// <summary>
    /// Remove the first node that contains 'value'.
    /// </summary>
    public void Remove(int value)
    {
        // Problem 3: Implement Remove
        Node? curr = _head; // Comenzar desde la cabeza de la lista

        while (curr is not null)
        {
            if (curr.Data == value)
            {
                if (curr == _head)
                {
                    // Si el nodo a remover es la cabeza, llamar a RemoveHead
                    RemoveHead();
                }
                else if (curr == _tail)
                {
                    // Si el nodo a remover es la cola, llamar a RemoveTail
                    RemoveTail();
                }
                else
                {
                    // Si el nodo está en el medio, ajustar los punteros de los nodos adyacentes
                    curr.Prev!.Next = curr.Next; // Conectar el nodo anterior al siguiente nodo
                    curr.Next.Prev = curr.Prev; // Conectar el siguiente nodo al nodo anterior
                }

                return; // Salir después de remover el nodo
            }

            curr = curr.Next; // Continuar buscando el valor
        }
    }

    /// <summary>
    /// Search for all instances of 'oldValue' and replace the value to 'newValue'.
    /// </summary>
    public void Replace(int oldValue, int newValue)
    {
        // Problem 4: Implement Replace
        Node? curr = _head; // Comenzar desde la cabeza de la lista

        while (curr is not null)
        {
            if (curr.Data == oldValue)
            {
                curr.Data = newValue; // Reemplazar el valor del nodo actual
            }

            curr = curr.Next; // Continuar buscando otras instancias
        }
    }

    /// <summary>
    /// Yields all values in the linked list
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        // Call the generic version of the method
        return this.GetEnumerator();
    }

    /// <summary>
    /// Iterate forward through the Linked List
    /// </summary>
    public IEnumerator<int> GetEnumerator()
    {
        var curr = _head; // Start at the beginning since this is a forward iteration.
        while (curr is not null)
        {
            yield return curr.Data; // Provide (yield) each item to the user
            curr = curr.Next; // Go forward in the linked list
        }
    }

    /// <summary>
    /// Iterate backward through the Linked List
    /// </summary>
    public IEnumerable<int> Reverse()
    {
        // Problem 5: Implement Reverse
        var curr = _tail; // Comenzar desde la cola para iterar hacia atrás
        while (curr is not null)
        {
            yield return curr.Data; // Proporcionar (yield) cada valor al usuario
            curr = curr.Prev; // Ir hacia atrás en la lista enlazada
        }
    }

    public override string ToString()
    {
        return "<LinkedList>{" + string.Join(", ", this) + "}";
    }

    // Just for testing.
    public bool HeadAndTailAreNull()
    {
        return _head is null && _tail is null;
    }

    // Just for testing.
    public bool HeadAndTailAreNotNull()
    {
        return _head is not null && _tail is not null;
    }

    /// <summary>
    /// Clase interna para representar un nodo en la lista enlazada.
    /// </summary>
    private class Node
    {
        public int Data { get; set; }
        public Node? Next { get; set; }
        public Node? Prev { get; set; }

        public Node(int data)
        {
            Data = data;
            Next = null;
            Prev = null;
        }
    }
}

public static class IntArrayExtensionMethods
{
    public static string AsString(this IEnumerable array)
    {
        return "<IEnumerable>{" + string.Join(", ", array.Cast<int>()) + "}";
    }
}
