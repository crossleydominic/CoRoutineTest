using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CoRoutineTest
{
	public class CopyFileAsync : Coroutine
	{
		private const int BUFFER_SIZE = 409600;

		private string _fileName;
		private string _destinationFolder;
		private byte[] _buffer;

		public CopyFileAsync(string fileName, string destinationFolder)
		{
			_fileName = fileName;
			_destinationFolder = destinationFolder;
			_buffer = new byte[BUFFER_SIZE];
		}

		public override IEnumerator<CoroutineState> GetEnumerator()
		{
			FileStream fileReader = new FileStream(
				_fileName,
				 FileMode.Open,
				 FileAccess.Read,
				 FileShare.Read,
				 BUFFER_SIZE,
				 true);

			FileStream fileWriter = new FileStream(
				Path.Combine(_destinationFolder, Path.GetFileName(_fileName)),
				FileMode.Create,
				FileAccess.ReadWrite,
				FileShare.Write,
				BUFFER_SIZE,
				true);

			int bytesRead = 0;

			do
			{
				//Read asynchronously
				bool finishedRead = false;
				IAsyncResult readResult = fileReader.BeginRead(_buffer, 0, BUFFER_SIZE,
					(asyncResult) =>
					{
						bytesRead = fileReader.EndRead(asyncResult);
						finishedRead = true;
					},
					null);

				//Wait until the reading is complete
				while (!finishedRead)
				{
					//Allow other coroutines to run 
					yield return CoroutineState.Running;
				}
				Console.WriteLine("Finished reading chunk for: " + _fileName);

				//Write asynchronously
				bool finishedWriting = false;
				IAsyncResult writeResult = fileWriter.BeginWrite(_buffer, 0, bytesRead,
					(asyncResult) =>
					{
						fileWriter.EndWrite(asyncResult);
						finishedWriting = true;
					},
					null);

				//Wait until write is finished
				while (!finishedWriting)
				{
					//Let other coroutines run
					yield return CoroutineState.Running;
				}
				Console.WriteLine("Finished writing chunk for: " + _fileName);

			} while (bytesRead > 0);

			fileReader.Close();
			fileWriter.Close();
		}
	}
}
