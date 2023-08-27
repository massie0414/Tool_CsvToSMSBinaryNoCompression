using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    //const int WIDTH = 256;  // 最大値
    //const int HEIGHT = 256;  // 最大値

    const string INPUT_FILE = "convert.csv";
    //const string OUTPUT_FILE = "bg (tilemap).bin";


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

        // 1バイトずつ読み出し。
        //using (BinaryReader w = new BinaryReader(File.OpenRead(fileName)))
        //int fileIndex = 0;
        foreach (string line in File.ReadLines(fileName))
        {
            try
            {
                string[] arr = line.Split(',');
                for (int i = 0; i < arr.Length; i++)
                {
                    ints[index] = Int32.Parse(arr[i]);
                    index++;
                }
                //fileIndex++;
                //index = 0;
            }
            catch (EndOfStreamException)
            {
                Console.Write("\n");
            }
        }


        //int tile = -1;
        //int num = -1;

        //int[] endPoint = new int[HEIGHT];
        //int writeCount = 0;

        string outFileName = fileName.Split('.')[0];

        //Console.Out.WriteLine(fileName);
        //Console.Out.WriteLine(outFileName);

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


        /*
        using (Stream stream = File.OpenWrite(OUTPUT_FILE))
        {
            // streamに書き込むためのBinaryWriterを作成
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                for (int y = 0; y < HEIGHT; y++)
                {
                    endPoint[y] = writeCount;

                    for (int x = 0; x < WIDTH; x++)
                    {
                        if (ints[y, x] >= 7)
                        {
                            if (
                                tile >= 0 && tile <= 6  // 前回も０～６
                                && num > -1
                            )
                            {
                                // 前回と違うタイル

                                byte w2 = (byte)(tile * 32 + num);

                                writer.Write((byte)w2);
                                writeCount++;

                                // リセット
                                tile = -1;
                                num = -1;
                            }


                            // 7以上ならそのまま書き込む
                            num = ints[y, x] - 7;  // numの領域には6を引いた数を入れる

                            // 保存
                            //Console.Write("tile=" + tile + " num=" + num + " ");

                            byte w = (byte)(224 + num);

                            writer.Write((byte)w);
                            writeCount++;

                            // リセット
                            tile = -1;
                            num = -1;
                        }
                        else
                        {
                            // 6以下のタイル

                            if (
                                tile >= 0 && tile <= 6  // 前回も０～６
                                && tile != ints[y, x] // 前回と違うタイル
                                && num > -1
                            )
                            {
                                // 前回と違うタイル

                                // 保存
                                //Console.Write("tile=" + tile + " num=" + num + " ");

                                byte w = (byte)(tile * 32 + num);

                                writer.Write((byte)w);
                                writeCount++;

                                // リセット
                                tile = -1;
                                num = -1;

                            }

                            tile = ints[y, x];
                            num++;

                            // タイルが32個連続で並んだ
                            if (num >= 31)
                            {
                                // いったん保存
                                //Console.Write("tile=" + tile + " num=" + num + " ");

                                byte w = (byte)(tile * 32 + num);

                                writer.Write((byte)w);
                                writeCount++;

                                // リセット
                                tile = -1;
                                num = -1;
                            }
                        }
                    }

                    // ここでもいったんリセット
                    if (num >= 0 && 0 <= tile && tile <= 6)
                    {
                        byte w = (byte)(tile * 32 + num);

                        writer.Write((byte)w);
                        writeCount++;

                        // リセット
                        tile = -1;
                        num = -1;

                    }
                }
            }
        }
        */

        /*
        using (Stream stream = File.OpenWrite(ENDPOINT_FILE))
        {
            // streamに書き込むためのBinaryWriterを作成
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                for (int i = 0; i < HEIGHT; i++)
                {
                    writer.Write((byte)(endPoint[i] / HEIGHT));
                    writer.Write((byte)(endPoint[i] % HEIGHT));
                }
            }

        }
        */

    }

}
