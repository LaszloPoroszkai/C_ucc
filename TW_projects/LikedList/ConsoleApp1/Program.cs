using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedOnce list = new LinkedOnce();
            list.InsertFront("1st Element");
            list.InsertLast("2nd Element");
            list.InsertLast("3rd Element");
            list.InsertFront("0 Element");

            if (list.getHead().getNext() != null)
            {
                printNode(list.getHead());
            }
        }

        public static void printNode(Node node)
        {
            Console.WriteLine(node.getData());
            if (node.getNext() != null)
            {
                printNode(node.getNext());
            }
        }
    }

    class Node
    {
        private Node next;
        private String data;

        public Node(String data)
        {
            this.data = data;
            next = null;
        }

        public Node getNext()
        {
            return next;
        }

        public void setNext(Node newNode)
        {
            this.next = newNode;
        }

        public String getData()
        {
            return data;
        }
    }

    class LinkedOnce
    {
        private Node head;
        private Node last;

        public void InsertFront(String data)
        {
            Node new_node = new Node(data);
            new_node.setNext(this.head);
            if (this.last == null)
            {
                this.last = new_node;
            }
            this.head = new_node;
        }

        public void InsertLast(String data)
        {
            Node new_node = new Node(data);
            if (this.head == null)
            {
                this.head = new_node;
                return;
            }
            this.last.setNext(new_node);
            this.last = new_node;
        }

        public Node getHead()
        {
            return head;
        }
    }

}
