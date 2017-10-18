using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace CJ_Controls.Communication
{
	public class TCP_IP_Socket : IDisposable
	{
		private Socket _socket;
		private string _host;
		private int _port;
		private bool _connected;
		public bool IsConnected
		{
			get { return _connected; }
		}
		private EventWaitHandle _closeEvent = new EventWaitHandle(false, EventResetMode.ManualReset);
		public event EventHandler OnRcvData = null;
		public TCP_IP_Socket(string strIp, int nPort)
		{
			_host = strIp;
			_port = nPort;
		}

		public int SendTimeout
		{
			get
			{
				if (_connected == false)
				{
					return Timeout.Infinite;
				}
				return (int)_socket.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout);
			}
			set
			{
				if (_connected == false)
				{
					return;
				}
				_socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, value);
			}
		}

		public int ReceiveTimeout
		{
			get
			{
				if (_connected == false)
				{
					return Timeout.Infinite;
				}

				return (int)_socket.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout);
			}
			set
			{
				if (_connected == false)
				{
					return;
				}

				_socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, value);
			}
		}
		public bool Connect(string strIp, int nPort)
		{
			_host = strIp;
			_port = nPort;

			return Connect();
		}
		public bool Connect()
		{
			try
			{
				if (_socket != null)
				{
					Close();
				}

				_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				_socket.Connect(_host, _port);
				_connected = true;

				Receive(_socket);

				OnConnected();
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Tcp_Ip_Socket:"+ex.Message);
				Close();
			}

			return false;
		}

		private void Receive(Socket client)
		{
			try
			{
				// Create the state object.
				StateObject state = new StateObject();
				state.workSocket = client;

				// Begin receiving the data from the remote device.
				client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
					new AsyncCallback(OnReceive), state);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
		}
		private void OnReceive(IAsyncResult ar)
		{
			try
			{
				// Retrieve the state object and the client socket 
				// from the asynchronous state object.
				StateObject state = (StateObject)ar.AsyncState;
				Socket client = state.workSocket;

				// Read data from the remote device.
				int bytesRead = client.EndReceive(ar);

				if (bytesRead > 0)
				{
					byte[] byteRecvData = new byte[bytesRead];
					Buffer.BlockCopy(state.buffer, 0, byteRecvData, 0, bytesRead);

					if (OnRcvData != null)
					{
						OnRcvData(byteRecvData, EventArgs.Empty);
					}
					Array.Clear(state.buffer, 0, state.buffer.Length);
					client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
						new AsyncCallback(OnReceive), state);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
		}

		public void Close()
		{
			bool fireLostEvent = _connected == true;

			_connected = false;

			if (_closeEvent != null)
			{
				try
				{
					_closeEvent.Set();
					_closeEvent.Dispose();
				}
				catch { }

				_closeEvent = null;
			}

			if (_socket != null)
			{
				try
				{
					_socket.Close();
					_socket.Dispose();
					_socket = null;
				}
				catch { }

				if (fireLostEvent == true)
				{
					OnConnectionLost();
				}
			}
		}

		public event Action Connected = null;
		protected virtual void OnConnected()
		{
			Debug.WriteLine("Connected.");

			if (Connected != null)
			{
				Connected();
			}
		}

		public event Action ConnectionLost = null;
		protected virtual void OnConnectionLost()
		{
			Debug.WriteLine("Connection lost");
			if (ConnectionLost != null)
			{
				ConnectionLost();
			}
		}

		public int Write(byte[] buffer)
		{
			if (_connected == false)
			{
				return 0;
			}

			int offset = 0;
			int size = buffer.Length;

			int totalSent = 0;

			while (true)
			{
				int sent = 0;

				try
				{
					sent = _socket.Send(buffer, offset, size, SocketFlags.None);
				}
				catch (SocketException ex)
				{
					if (ex.SocketErrorCode == SocketError.TimedOut)
					{
						break;
					}

					Close();
					break;
				}

				totalSent += sent;

				if (totalSent == buffer.Length)
				{
					break;
				}

				offset += sent;
				size -= sent;
			}

			return totalSent;
		}

		public int Read(byte[] readBuf)
		{
			return Read(readBuf, 0, readBuf.Length);
		}

		public int Read(byte[] buffer, int offset, int size)
		{
			if (_connected == false)
			{
				return 0;
			}

			if (size == 0)
			{
				return 0;
			}

			int totalRead = 0;
			int readRemains = size;

			while (true)
			{
				int readLen = 0;

				try
				{
					readLen = _socket.Receive(buffer, offset, readRemains, SocketFlags.None);
				}
				catch (SocketException ex)
				{
					if (ex.SocketErrorCode == SocketError.TimedOut)
					{
						break;
					}
				}

				if (readLen == 0)
				{
					Close();
					break;
				}

				totalRead += readLen;

				if (totalRead == size)
				{
					break;
				}

				offset += readLen;
				readRemains -= readLen;
			}

			return totalRead;
		}

		void IDisposable.Dispose()
		{
			Close();
		}
	}

	public class StateObject
	{
		// Client socket.
		public Socket workSocket = null;
		// Size of receive buffer.
		public const int BufferSize = 2048;
		// Receive buffer.
		public byte[] buffer = new byte[BufferSize];
	}
}
