using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    const string INPUT_FILE = "convert.csv";

    static void Main(string[] args)
    {
        string fileName = INPUT_FILE;
        if (args.Length >= 1)
        {
            string[] pathName = args[0].Split('\\');
            fileName = pathName[pathName.Length - 1];
        }

        int byteNum = 1;
        if (args.Length >= 2)
        {
            byteNum = Int32.Parse(args[1]);
        }

        // ファイルサイズの取得
        FileInfo file = new FileInfo(fileName);
        long fileSize = file.Length;
        int file_end_address = (int)fileSize - 1;

        int[] ints = new int[file_end_address];
        int index = 0;

        int fileWidth = 0;
        int fileHeight = 0;

        // 1バイトずつ読み出し。
        foreach (string line in File.ReadLines(fileName))
        {
            try
            {
                string[] arr = line.Split(',');
                fileWidth = 0;
                for (int i = 0; i < arr.Length; i++)
                {
                    ints[index] = Int32.Parse(arr[i]);
                    index++;
                    fileWidth++;
                }
            }
            catch (EndOfStreamException)
            {
                Console.Write("\n");
            }
            fileHeight++;
        }

        string outFileName = fileName.Split('.')[0];

        // ファイル書き込み
        using (Stream stream = File.OpenWrite(outFileName + ".bin"))
        {
            // streamに書き込むためのBinaryWriterを作成
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                for (int i = 0; i < index; i++)
                {
                    if (byteNum == 1)
                    {
                        writer.Write((byte)(ints[i]));
                    }
                    else if (byteNum==2) {
                        writer.Write((byte)(ints[i]%256));
                        writer.Write((byte)(ints[i]/256));
                    }
                }
            }
        }

        using (Stream stream = File.OpenWrite(outFileName + "_width.bin"))
        {
            // streamに書き込むためのBinaryWriterを作成
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write((byte)(fileWidth));
            }
        }

        using (Stream stream = File.OpenWrite(outFileName + "_height.bin"))
        {
            // streamに書き込むためのBinaryWriterを作成
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write((byte)(fileHeight));
            }
        }

    }

}
