using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman
{
    public class Program
    {
        class HuffmanNode
        {
            public char Symbol { get; set; }
            public int Frequency { get; set; }
            public HuffmanNode Left { get; set; }
            public HuffmanNode Right { get; set; }
        }
        class Huffman
        {
            private List<HuffmanNode> nodes = new List<HuffmanNode>();
            public HuffmanNode Root { get; private set; }
            public Dictionary<char, string> HuffmanCode { get; private set; }

            public void Build(string input)
            {
                // Bước 1: Tạo bảng tần suất xuất hiện của các ký tự
                var frequencyTable = new Dictionary<char, int>();
                foreach (var symbol in input)
                {
                    if (!frequencyTable.ContainsKey(symbol))
                        frequencyTable[symbol] = 0;

                    frequencyTable[symbol]++;
                }

                // Bước 2: Tạo các nút Huffman cho mỗi ký tự và thêm vào danh sách các nút
                foreach (var symbol in frequencyTable)
                {
                    nodes.Add(new HuffmanNode() { Symbol = symbol.Key, Frequency = symbol.Value });
                }

                // Bước 3: Xây dựng cây Huffman
                while (nodes.Count > 1)
                {
                    // Sắp xếp các nút theo tần suất xuất hiện (tăng dần)
                    nodes.Sort((x, y) => x.Frequency - y.Frequency);

                    // Chọn hai nút có tần suất nhỏ nhất
                    var left = nodes[0];
                    var right = nodes[1];

                    // Tạo nút cha và gán hai nút con vào
                    var parent = new HuffmanNode()
                    {
                        Symbol = '*',
                        Frequency = left.Frequency + right.Frequency,
                        Left = left,
                        Right = right
                    };

                    // Loại bỏ hai nút nhỏ nhất và thêm nút cha vào danh sách
                    nodes.Remove(left);
                    nodes.Remove(right);
                    nodes.Add(parent);
                }

                // Nút còn lại là nút gốc của cây Huffman
                Root = nodes[0];

                // Bước 4: Tạo mã Huffman cho mỗi ký tự
                HuffmanCode = new Dictionary<char, string>();
                BuildHuffmanCode(Root, "");
            }

            private void BuildHuffmanCode(HuffmanNode node, string code)
            {
                if (node == null) return;

                // Nếu là nút lá (ký tự không phải '*'), gán mã Huffman cho ký tự
                if (!node.Symbol.Equals('*'))
                {
                    HuffmanCode[node.Symbol] = code;
                }

                // Duyệt cây để tạo mã Huffman (trái: 0, phải: 1)
                BuildHuffmanCode(node.Left, code + "0");
                BuildHuffmanCode(node.Right, code + "1");
            }

            public string Encode(string input)
            {
                StringBuilder output = new StringBuilder();
                foreach (var symbol in input)
                {
                    output.Append(HuffmanCode[symbol]);
                }
                return output.ToString();
            }

            public string Decode(string encoded)
            {
                StringBuilder output = new StringBuilder();
                var current = Root;
                foreach (var bit in encoded)
                {
                    current = bit == '0' ? current.Left : current.Right;

                    // Nếu là nút lá, thêm ký tự vào chuỗi kết quả
                    if (current.Left == null && current.Right == null)
                    {
                        output.Append(current.Symbol);
                        current = Root;
                    }
                }
                return output.ToString();
            }
        }
        static void Main(string[] args)
        {
            string input = "Let me register for the course";

            Huffman huffman = new Huffman();
            huffman.Build(input);

            Console.WriteLine("Symbol\tHuffman Code");
            foreach (var symbol in huffman.HuffmanCode)
            {
                Console.WriteLine($"{symbol.Key}\t{symbol.Value}");
            }

            string encoded = huffman.Encode(input);
            Console.WriteLine($"\nEncoded:\n{encoded}");

            string decoded = huffman.Decode(encoded);
            Console.WriteLine($"\nDecoded:\n{decoded}");
            Console.ReadLine();

        }
    }
}
