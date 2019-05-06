using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Net;
using System;

/// <summary>
/// 通过http下载资源
/// </summary>

namespace UFrame.Http
{
    public class HttpDownLoad
    {
        //下载进度
        public float progress { get; private set; }
        //涉及子线程要注意,Unity关闭的时候子线程不会关闭，所以要有一个标识
        private bool isStop;
        //子线程负责下载，否则会阻塞主线程，Unity界面会卡主
        private Thread thread;
        //表示下载是否完成
        public bool isDone { get; private set; }
        const int ReadWriteTimeOut = 2 * 1000;//超时等待时间
        const int TimeOutWait = 5 * 1000;//超时等待时间

        /// <summary>
        /// 单文件下载方法(支持断点续传)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="savePath"></param>
        /// <param name="fileName"></param>
        /// <param name="isNewFile">true不断点续传</param>
        /// <param name="callBack"></param>
        /// <param name="threadPriority"></param>
        public void DownLoad(string url, string savePath, string fileName, bool isNewFile, Action callBack, System.Threading.ThreadPriority threadPriority = System.Threading.ThreadPriority.Normal)
        {
            isStop = false;
            System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
            //开启子线程下载,使用匿名方法
            thread = new Thread(delegate ()
            {
                stopWatch.Start();
                //判断保存路径是否存在
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

                //获取下载文件的总长度
                UnityEngine.Debug.Log(url + " " + fileName);
                long totalLength = GetLength(url);

                //获取文件现在的长度
                string filePath = savePath + "/" + fileName;
                FileStream fs = null;
                if (isNewFile)
                {
                    //覆盖下载
                    fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                }
                else
                {
                    //断点续传
                    fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                }
                long fileLength = fs.Length;
                Logger.LogWarp.LogFormat("文件:{0} 已下载{1}，剩余{2}", fileName, fileLength, (totalLength - fileLength));
                //如果没下载完
                if (fileLength < totalLength)
                {
                    //断点续传核心，设置本地文件流的起始位置
                    fs.Seek(fileLength, SeekOrigin.Begin);

                    HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
                    request.ReadWriteTimeout = ReadWriteTimeOut;
                    request.Timeout = TimeOutWait;

                    //断点续传核心，设置远程访问文件流的起始位置
                    request.AddRange((int)fileLength);
                    Stream stream = null;
                    try
                    {
                        stream = request.GetResponse().GetResponseStream();
                    }
                    catch (Exception ex)
                    {
                        Logger.LogWarp.LogError(ex.ToString());
                    }
                    byte[] buffer = new byte[1024];
                    //使用流读取内容到buffer中
                    //注意方法返回值代表读取的实际长度,并不是buffer有多大，stream就会读进去多少
                    int length = stream.Read(buffer, 0, buffer.Length);
                    while (length > 0)
                    {
                        //如果Unity客户端关闭，停止下载
                        if (isStop) break;
                        //将内容再写入本地文件中
                        fs.Write(buffer, 0, length);
                        //计算进度
                        fileLength += length;
                        progress = (float)fileLength / (float)totalLength;
                        //类似尾递归
                        length = stream.Read(buffer, 0, buffer.Length);

                    }
                    stream.Close();
                    stream.Dispose();
                }
                else
                {
                    progress = 1;
                }
                stopWatch.Stop();
                Logger.LogWarp.Log("耗时: " + stopWatch.ElapsedMilliseconds);
                fs.Close();
                fs.Dispose();
                //如果下载完毕，执行回调
                if (progress == 1)
                {
                    isDone = true;
                    if (callBack != null) callBack();
                    Logger.LogWarp.Log(url + " download finished");
                    thread.Abort();
                }

            });
            //开启子线程
            thread.IsBackground = true;
            thread.Priority = threadPriority;
            thread.Start();
        }

        /// <summary>
        /// 下载多个文件
        /// </summary>
        /// <param name="urls"></param>
        /// <param name="savePath"></param>
        /// <param name="isNewFile"></param>
        /// <param name="callBack"></param>
        /// <param name="threadPriority"></param>
        public void DownLoads(Dictionary<string, string> urls, string savePath, bool isNewFile, Action callBack, System.Threading.ThreadPriority threadPriority = System.Threading.ThreadPriority.Normal)
        {
            isStop = false;
            System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
            //开启子线程下载,使用匿名方法
            thread = new Thread(delegate ()
            {
                stopWatch.Start();
                //判断保存路径是否存在
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }
                int idx = 0;
                foreach (var kv in urls)
                {
                    //获取下载文件的总长度
                    //UnityEngine.Debug.Log(kv.k + " " + fileName);
                    long totalLength = GetLength(kv.Key);

                    //获取文件现在的长度
                    string filePath = savePath + "/" + kv.Value;
                    FileStream fs = null;
                    if (isNewFile)
                    {
                        //覆盖下载
                        fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                    }
                    else
                    {
                        //断点续传
                        fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                    }
                    long fileLength = fs.Length;
                    Logger.LogWarp.LogFormat("文件:{0} 已下载{1}，剩余{2}", kv.Value, fileLength, (totalLength - fileLength));
                    //如果没下载完
                    if (fileLength < totalLength)
                    {
                        //断点续传核心，设置本地文件流的起始位置
                        fs.Seek(fileLength, SeekOrigin.Begin);

                        HttpWebRequest request = HttpWebRequest.Create(kv.Key) as HttpWebRequest;
                        request.ReadWriteTimeout = ReadWriteTimeOut;
                        request.Timeout = TimeOutWait;

                        //断点续传核心，设置远程访问文件流的起始位置
                        request.AddRange((int)fileLength);
                        Stream stream = null;
                        try
                        {
                            stream = request.GetResponse().GetResponseStream();
                        }
                        catch (Exception ex)
                        {
                            Logger.LogWarp.LogError(ex.ToString());
                        }
                        byte[] buffer = new byte[1024];
                        //使用流读取内容到buffer中
                        //注意方法返回值代表读取的实际长度,并不是buffer有多大，stream就会读进去多少
                        int length = stream.Read(buffer, 0, buffer.Length);
                        while (length > 0)
                        {
                            //如果Unity客户端关闭，停止下载
                            if (isStop) break;
                            //将内容再写入本地文件中
                            fs.Write(buffer, 0, length);
                            //计算进度
                            fileLength += length;
                            //progress = (float)fileLength / (float)totalLength;
                            //类似尾递归
                            length = stream.Read(buffer, 0, buffer.Length);

                        }
                        stream.Close();
                        stream.Dispose();
                        idx++;
                        progress = (float)idx / (float)urls.Count;
                        Logger.LogWarp.Log("progress " + progress);
                    }
                    else
                    {
                        //progress = 1;
                        idx++;
                        progress = (float)idx / (float)urls.Count;
                        Logger.LogWarp.Log("progress " + progress);
                    }
                    stopWatch.Stop();
                    Logger.LogWarp.Log("耗时: " + stopWatch.ElapsedMilliseconds);
                    fs.Close();
                    fs.Dispose();
                    //如果下载完毕，执行回调
                    if (progress >= 1)
                    {
                        isDone = true;
                        if (callBack != null) callBack();
                        Logger.LogWarp.Log(urls.Count + " download finished");
                        thread.Abort();
                    }
                }
            });


            //开启子线程
            thread.IsBackground = true;
            thread.Priority = threadPriority;
            thread.Start();
        }


        /// <summary>
        /// 获取下载文件的大小
        /// </summary>
        /// <returns>The length.</returns>
        /// <param name="url">URL.</param>
        long GetLength(string url)
        {
            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
            request.Method = "HEAD";
            request.ReadWriteTimeout = ReadWriteTimeOut;
            request.Timeout = TimeOutWait;
            HttpWebResponse response = null;
            try
            {
                response = request.GetResponse() as HttpWebResponse;
            }
            catch (Exception ex)
            {
                Logger.LogWarp.LogError(ex.ToString());
            }

            return response.ContentLength;
        }

        public void Close()
        {
            isStop = true;
        }

    }
}

