using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Uploadtest
{
	class Program
	{
		static void Main(string[] args)
		{

			FileStream fs = new FileStream(System.AppDomain.CurrentDomain.BaseDirectory + "uploadimg.jpg", FileMode.Open, FileAccess.Read);
			
				byte[] buffur = new byte[fs.Length];
				fs.Read(buffur, 0, (int)fs.Length);
					//关闭资源  
			   fs.Close();

			   string sUrl = "http://localhost:16912/Api/ImgUpload/Uplaod";
			   //HttpWebRequest request = HttpWebRequest.Create(sUrl) as HttpWebRequest;
			   //request.ContentLength = buffur.Length;
			   //request.Method = "POST";
			   //request.ContentType = "binary/octet-stream";
			   //request.GetRequestStream().Write(buffur, 0, buffur.Length);
			   //request.GetResponse();

			   string boundary = "---------------" + DateTime.Now.Ticks.ToString("x");//分隔符
			   var beginBoundary = Encoding.ASCII.GetBytes("--" + boundary + "\r\n");
			   var endBoundary = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
			   var seperate = Encoding.ASCII.GetBytes("\r\n");

			   HttpWebRequest request = HttpWebRequest.Create(sUrl) as HttpWebRequest;
			   //设置超时时间为3000秒
			   request.Timeout = 3000 * 1000;
			   request.Method = "POST";
			   request.ContentType = "multipart/form-data; boundary=" + boundary;
			   var reqStream = request.GetRequestStream();
			   string filePartHeader =
				"Content-Disposition:form-data;name=\"{0}\";filename=\"{1}\"\r\n" +
				 "Content-Type:{2}\r\n\r\n";
			   byte[] buffer = new byte[1024];
			   reqStream.Write(seperate, 0, seperate.Length);

			   reqStream.Write(beginBoundary, 0, beginBoundary.Length);
			   var header = string.Format(filePartHeader, "FileData", "PIC.jpg", "image/jpeg");
			   var headerbytes = Encoding.UTF8.GetBytes(header);
			   reqStream.Write(headerbytes, 0, headerbytes.Length);
			 //  int bytesRead;

			   //while ((bytesRead = streamReceive.Read(buffer, 0, buffer.Length)) > 0)
			   //{
			   reqStream.Write(buffur, 0, buffur.Length);
			   //}

			   reqStream.Write(seperate, 0, seperate.Length);
			   reqStream.Write(beginBoundary, 0, beginBoundary.Length);
			   //var pathParamBytes = Encoding.UTF8.GetBytes(string.Format("Content-Disposition: form-data; name=\"company\"\r\n\r\n{0}", Function.GetLogUser().i_Company));
			   //reqStream.Write(pathParamBytes, 0, pathParamBytes.Length);

			   reqStream.Write(endBoundary, 0, endBoundary.Length);
			   reqStream.Close();
			   //将封装的请求发送到目标服务器         
			   var response = (HttpWebResponse)request.GetResponse();
			   string responseContent;
			   using (var httpStreamReader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")))
			   {
				   responseContent = httpStreamReader.ReadToEnd();
			   }


			//   string boundary = "---------------" + DateTime.Now.Ticks.ToString("x");//分隔符
			//   //var beginBoundary = Encoding.ASCII.GetBytes("--" + boundary + "\r\n");
			//   //var endBoundary = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
			//   //var seperate = Encoding.ASCII.GetBytes("\r\n");


			// StringBuilder sb = new StringBuilder();  
			//sb.Append("--");  
			//sb.Append(boundary);  
			//sb.Append("\r\n");  
			//sb.Append("Content-Disposition: form-data; name=\"");  
			//sb.Append("file");  
			//sb.Append("\"; filename=\"");  
			//sb.Append("file.data");  
			//sb.Append("\"");  
			//sb.Append("\r\n");  
			//sb.Append("Content-Type: ");  
			//sb.Append("application/octet-stream");  
			//sb.Append("\r\n");  
			//sb.Append("\r\n");  
			//string strPostHeader = sb.ToString();  
			//byte[] postHeaderBytes = Encoding.UTF8.GetBytes(strPostHeader);  
			//byte[] boundaryBytes = Encoding.UTF8.GetBytes(boundary);  

			//// 根据uri创建HttpWebRequest对象   
			//HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(new Uri(sUrl));  
			//httpReq.Method = "POST";  
   
			////对发送的数据不使用缓存   
			//httpReq.AllowWriteStreamBuffering = false;  
   
			////设置获得响应的超时时间（300秒）   
			//httpReq.Timeout = 300000;  
			//httpReq.ContentType = "multipart/form-data; boundary=" + boundary;


			//long length = buffur.Length + postHeaderBytes.Length + boundaryBytes.Length;
			//long fileLength = buffur.Length;  
			//httpReq.ContentLength = length;  
			////try 
			////{  
              
			//	Stream postStream = httpReq.GetRequestStream();  
   
			//	//发送请求头部消息   
			//	postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
			//	postStream.Write(buffur, 0, buffur.Length);
			//	postStream.Write(boundaryBytes, 0, boundaryBytes.Length);
		    
			//HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(sUrl);
			//webRequest.ProtocolVersion = HttpVersion.Version10;
			//webRequest.Timeout = 30000;
			//webRequest.Method = "POST";
			//webRequest.UserAgent = "Mozilla/4.0";
			////webRequest.Headers.Add("Accept-Encoding", "gzip, deflate");
			//webRequest.ContentType = "application/x-www-form-urlencoded";


		

		//	byte[] bPostData = Encoding.UTF8.GetBytes(buffur);//将字符串转化为字节 --编码方式为UTF8
			//if (buffur != null)
			//{//将数据写入请求中
			//	Stream postDataStream = webRequest.GetRequestStream();
			//	postDataStream.Write(buffur, 0, buffur.Length);
			//}

			//string sResult=string.Empty;
		    
			//HttpWebResponse webResponse = (System.Net.HttpWebResponse)webRequest.GetResponse();
			//using (Stream streamReceive = webResponse.GetResponseStream())
			//{
			//	using (StreamReader sr = new StreamReader(streamReceive,Encoding.UTF8))
			//	{
			//		sResult = sr.ReadToEnd();
			//	}
			//}



		}
	}
}




//int returnValue = 0;  
   
//			// 要上传的文件   
//			FileStream fs = new FileStream(fileNamePath, FileMode.Open, FileAccess.Read);  
//			BinaryReader r = new BinaryReader(fs);  
   
//			//时间戳   
//			string strBoundary = "----------" + DateTime.Now.Ticks.ToString("x");  
//			byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + strBoundary + "\r\n");  
   
//			//请求头部信息   
//			StringBuilder sb = new StringBuilder();  
//			sb.Append("--");  
//			sb.Append(strBoundary);  
//			sb.Append("\r\n");  
//			sb.Append("Content-Disposition: form-data; name=\"");  
//			sb.Append("file");  
//			sb.Append("\"; filename=\"");  
//			sb.Append(saveName);  
//			sb.Append("\"");  
//			sb.Append("\r\n");  
//			sb.Append("Content-Type: ");  
//			sb.Append("application/octet-stream");  
//			sb.Append("\r\n");  
//			sb.Append("\r\n");  
//			string strPostHeader = sb.ToString();  
//			byte[] postHeaderBytes = Encoding.UTF8.GetBytes(strPostHeader);  
   
//			// 根据uri创建HttpWebRequest对象   
//			HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(new Uri(address));  
//			httpReq.Method = "POST";  
   
//			//对发送的数据不使用缓存   
//			httpReq.AllowWriteStreamBuffering = false;  
   
//			//设置获得响应的超时时间（300秒）   
//			httpReq.Timeout = 300000;  
//			httpReq.ContentType = "multipart/form-data; boundary=" + strBoundary;  
//			long length = fs.Length + postHeaderBytes.Length + boundaryBytes.Length;  
//			long fileLength = fs.Length;  
//			httpReq.ContentLength = length;  
//			try 
//			{  
//				progressBar.Maximum = int.MaxValue;  
//				progressBar.Minimum = 0;  
//				progressBar.Value = 0;  
   
//				//每次上传4k   
//				int bufferLength = 4096;  
//				byte[] buffer = new byte[bufferLength];  
   
//				//已上传的字节数   
//				long offset = 0;  
   
//				//开始上传时间   
//				DateTime startTime = DateTime.Now;  
//				int size = r.Read(buffer, 0, bufferLength);  
//				Stream postStream = httpReq.GetRequestStream();  
   
//				//发送请求头部消息   
//				postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);  
//				while (size > 0)  
//				{  
//					postStream.Write(buffer, 0, size);  
//					offset += size;  
//					progressBar.Value = (int)(offset * (int.MaxValue / length));  
//					TimeSpan span = DateTime.Now - startTime;  
//					double second = span.TotalSeconds;  
//					lblTime.Text = "已用时：" + second.ToString("F2") + "秒";  
//					if (second > 0.001)  
//					{  
//						lblSpeed.Text = " 平均速度：" + (offset / 1024 / second).ToString("0.00") + "KB/秒";  
//					}  
//					else 
//					{  
//						lblSpeed.Text = " 正在连接…";  
//					}  
//					lblState.Text = "已上传：" + (offset * 100.0 / length).ToString("F2") + "%";  
//					lblSize.Text = (offset / 1048576.0).ToString("F2") + "M/" + (fileLength / 1048576.0).ToString("F2") + "M";  
//					Application.DoEvents();  
//					size = r.Read(buffer, 0, bufferLength);  
//				}  
//				//添加尾部的时间戳   
//				postStream.Write(boundaryBytes, 0, boundaryBytes.Length);  
//				postStream.Close();  
   
//				//获取服务器端的响应   
//				WebResponse webRespon = httpReq.GetResponse();  
//				Stream s = webRespon.GetResponseStream();  
//				StreamReader sr = new StreamReader(s);  
   
//				//读取服务器端返回的消息   
//				String sReturnString = sr.ReadLine();  
//				s.Close();  
//				sr.Close();  
//				if (sReturnString == "Success")  
//				{  
//					returnValue = 1;  
//				}  
//				else if (sReturnString == "Error")  
//				{  
//					returnValue = 0;  
//				}  
   
//			}  
//			catch(Exception ex)  
//			{  
//				returnValue = 0;  
//			}  
//			finally 
//			{  
//				fs.Close();  
//				r.Close();  
//			}  
   
//			return returnValue;  
//		}