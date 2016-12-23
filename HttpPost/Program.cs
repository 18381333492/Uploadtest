using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace HttpPost
{
	class Program
	{
	
		static void Main(string[] args)
		{
			//PostByForm();
			PostData();
		}

		//public enum ContentType
		//{
		//	 "image/jpeg"=1,
		//	 Data="",
		//}



		static void PostData()
		{

			string sUrl = "http://localhost:8888/Api/Values/Test?Name=tangtai";
			HttpWebRequest request = HttpWebRequest.Create(sUrl) as HttpWebRequest;
			request.Timeout = 3000 * 1000;
			request.Method = "POST";
            request.ContentType = "application/json";

            string dsf = "fdsfsdf";
            dsf = JsonConvert.SerializeObject("dsf");
            //
            //request.ContentType = "multipart/form-data; boundary=" + boundary; //这里是必须的
            //Stream RequestStream = request.GetRequestStream();

            //string data = "user=fdsf";
            //StringBuilder ContentHeader = new StringBuilder();
            //var dataByte = Encoding.UTF8.GetBytes(ContentHeader.ToString());

            //byte[] databyte = System.Text.Encoding.UTF8.GetBytes(data);
            //RequestStream.Write(databyte, 0, databyte.Length);
            //RequestStream.Close();

            //request.GetResponse();
            FileStream fs = new FileStream(System.AppDomain.CurrentDomain.BaseDirectory + "uploadimg.jpg", FileMode.Open, FileAccess.Read);

            byte[] buffur = new byte[fs.Length];
            fs.Read(buffur, 0, (int)fs.Length);
            //关闭资源  
            fs.Close();

            Stream rew = request.GetRequestStream();

            rew.Write(buffur, 0, buffur.Length);

            Stream s=request.GetResponse().GetResponseStream();//请求类型为POST的时候才可以设置请求的数据流,Get的时候会抛出异常

            StreamReader read = new StreamReader(s,Encoding.Unicode);
            //使用什么编码
            //接收的时候就用什么编码数据才会正常
            //相应解码



        //    byte[] bt = new byte[s.Length];

            string res = read.ReadToEnd();
               res= Encoding.Unicode.GetString(Encoding.Unicode.GetBytes(res.ToCharArray()));


            //    res = JsonConvert.DeserializeObject(res).ToString();

            //编码类型的处理
            //   res= Regex.Unescape(res);
            res= Regex.Escape(res);

            Convert.ToUInt32("f",16);

            var f=Encoding.Unicode.GetBytes("tangtai");
            var ll= Encoding.Unicode.GetString(f);
        }



		/// <summary>
		/// 通过表单的形式上传文件
		/// </summary>
		static void PostByForm()
		{
			FileStream fs = new FileStream(System.AppDomain.CurrentDomain.BaseDirectory + "uploadimg.jpg", FileMode.Open, FileAccess.Read);

			byte[] buffur = new byte[fs.Length];
			fs.Read(buffur, 0, (int)fs.Length);
			//关闭资源  
			fs.Close();

			string sUrl = "http://localhost:16912/Api/ImgUpload/Uplaod?Name=tangtai";

		
			//boundary是分隔符，
			//分隔多个文件、表单项。其中b372eb000e2 是即时生成的一个数字，用以确保整个分隔符不会在文件或表单项的内容中出现
			//Form每个部分用分隔符分割，
			//将分隔符写入文件头之前必须加上"--"着两个字符(即--{boundary})才能被http协议认为是Form的分隔符，
			/**表示结束的话用在正确的分隔符后面添加"--"表示结束 **/

			/***************************http post 请求的header格式：

			   POST/logsys/home/uploadIspeedLog!doDefault.html HTTP/1.1 

			Accept: text/plain, 
		   Accept-Language: zh-cn 
		   Host: 192.168.24.56
		   Content-Type:multipart/form-data;boundary=-----------------------------7db372eb000e2
		   User-Agent: WinHttpClient 
		   Content-Length: 3693
		   Connection: Keep-Alive

		   -------------------------------7db372eb000e2    开始分隔符

		   Content-Disposition: form-data; name="file"; filename="kn.jpg"

		   Content-Type: image/jpeg

		   (此处省略jpeg文件二进制数据...）

		   -------------------------------7db372eb000e2--	结束分隔符
			 * 
			 * 
			 * 
			*/

			//创建Http头的分隔符(可以是任意的字符串,只是为了区别不和上传的内容重复.一般情况下取时间戳)
			string boundary = "---------------" + DateTime.Now.Ticks.ToString("x");
			//获取分隔符的byte[]以写入RequestStream流中.
			var BoundaryByte = Encoding.ASCII.GetBytes("--" + boundary + "\r\n");//******  这里的换行不能少	********

			var EndBoundaryByte = Encoding.ASCII.GetBytes("--" + boundary + "--\r\n");//结束分隔符分隔符必需要多加“--”

			var newline = Encoding.ASCII.GetBytes("\r\n");

			StringBuilder FileHeader=new StringBuilder();
			FileHeader.AppendFormat("Content-Disposition:form-data;name=\"{0}\";filename=\"TT.jpg\"\r\n", "ImgFile");
			FileHeader.AppendFormat("Content-Type:{0}\r\n", "image/jpeg");//上传的文件类型
			//FileHeader.Append("Content-Transfer-Encoding:");//传输的编码
			var FileHeaderByte = Encoding.UTF8.GetBytes(FileHeader.ToString());

		//	byte[] FileByte = new byte[1024];

			HttpWebRequest request = HttpWebRequest.Create(sUrl) as HttpWebRequest;
			request.Timeout = 3000 * 1000;
			request.Method = "POST";
			request.ContentType = "multipart/form-data; boundary=" + boundary; //这里是必须的
			Stream RequestStream = request.GetRequestStream();

			RequestStream.Write(newline, 0, newline.Length);
			RequestStream.Write(BoundaryByte, 0, BoundaryByte.Length);	 //分隔符
			RequestStream.Write(FileHeaderByte, 0, FileHeaderByte.Length);//文件头内容
			RequestStream.Write(buffur, 0, buffur.Length);//文件内容
			RequestStream.Write(newline,0,newline.Length);
			RequestStream.Write(BoundaryByte, 0, BoundaryByte.Length);	 //分隔符
			RequestStream.Write(EndBoundaryByte, 0, EndBoundaryByte.Length); //结束分隔符



			RequestStream.Write(newline, 0, newline.Length);
			RequestStream.Write(BoundaryByte, 0, BoundaryByte.Length);	 //分隔符
		//	var FileHeaderByte = Encoding.UTF8.GetBytes(FileHeader.ToString());

	//		string data = "user=fdsf";
	//		StringBuilder ContentHeader = new StringBuilder();
	//		ContentHeader.AppendFormat("Content-Disposition:form-data;name=\"{0}\"","MM");
	////		ContentHeader.AppendFormat("Content-Type:{0}\r\n\r\n", "image/jpeg");//上传的文件类型
	//		var dataByte = Encoding.UTF8.GetBytes(ContentHeader.ToString());

	//		byte[] databyte= System.Text.Encoding.UTF8.GetBytes(data);

	//		RequestStream.Write(databyte, 0, databyte.Length);

	//		RequestStream.Write(newline, 0, newline.Length);
	//		RequestStream.Write(BoundaryByte, 0, BoundaryByte.Length);	 //分隔符
	//		RequestStream.Write(EndBoundaryByte, 0, EndBoundaryByte.Length); //结束分隔符
			//StringBuilder ContentHeader = new StringBuilder();
			//ContentHeader.AppendFormat("Content-Disposition:form-data;name=\"{0}\";filename=\"KK.jpg\"\r\n", "File");
			//ContentHeader.AppendFormat("Content-Type:{0}\r\n\r\n", "image/jpeg");//上传的文件类型
			//FileHeader.Append("Content-Transfer-Encoding:");//传输的编码
			//var ContentByte = Encoding.UTF8.GetBytes(ContentHeader.ToString());		
			RequestStream.Close();

			request.GetResponse();
            Stream s=request.GetRequestStream();

            StreamReader read = new StreamReader(s);
        //     request.GetRequestStream()

            string result = read.ReadToEnd();


        }
	}
}
