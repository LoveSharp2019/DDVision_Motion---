using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Design;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace VControls
{
	[Editor(typeof(FolderNameEditor), typeof(UITypeEditor))]
	[Description("提供一个Vista样式的选择文件对话框")]
	public class FolderBrowserDialog : Component
	{
		public FolderBrowserDialog()
		{

		}

		public string SelectedPath { get; set; }

		public string Description { get; set; }

		public DialogResult ShowDialog(IWin32Window owner=null)
		{
			IntPtr parent = (owner != null) ? owner.Handle : FolderBrowserDialog.GetActiveWindow();
			FolderBrowserDialog.IFileOpenDialog fileOpenDialog = (FolderBrowserDialog.IFileOpenDialog)new FolderBrowserDialog.FileOpenDialog();
			DialogResult result;
			try
			{
				if (!string.IsNullOrEmpty(this.SelectedPath))
				{
					uint num = 0u;
					IntPtr intptr_;
					FolderBrowserDialog.IShellItem shellItem;
					if (FolderBrowserDialog.SHILCreateFromPath(this.SelectedPath, out intptr_, ref num) == 0 && FolderBrowserDialog.SHCreateShellItem(IntPtr.Zero, IntPtr.Zero, intptr_, out shellItem) == 0)
					{
						fileOpenDialog.SetFolder(shellItem);
					}
				}
				fileOpenDialog.SetOptions((FolderBrowserDialog.Enum1)96);
				if (!string.IsNullOrEmpty(this.Description))
				{
					fileOpenDialog.SetTitle(this.Description);
				}
				uint num2 = fileOpenDialog.Show(parent);
				if (num2 == 2147943623u)
				{
					result = DialogResult.Cancel;
				}
				else if (num2 > 0u)
				{
					result = DialogResult.Abort;
				}
				else
				{
					FolderBrowserDialog.IShellItem shellItem;
					fileOpenDialog.GetResult(out shellItem);
					string selectedPath;
					shellItem.GetDisplayName((FolderBrowserDialog.Enum0)2147844096u, out selectedPath);
					this.SelectedPath = selectedPath;
					result = DialogResult.OK;
				}
			}
			finally
			{
				Marshal.ReleaseComObject(fileOpenDialog);
			}
			return result;
		}

		[DllImport("shell32.dll")]
		private static extern int SHILCreateFromPath([MarshalAs(UnmanagedType.LPWStr)] string string_2, out IntPtr intptr_0, ref uint uint_0);

		[DllImport("shell32.dll")]
		private static extern int SHCreateShellItem(IntPtr intptr_0, IntPtr intptr_1, IntPtr intptr_2, out FolderBrowserDialog.IShellItem ishellItem_0);

		[DllImport("user32.dll")]
		private static extern IntPtr GetActiveWindow();


		[Guid("DC1C5A9C-E88A-4dde-A5A1-60F82A20AEF7")]
		[ComImport]
		private class FileOpenDialog
		{

		}

		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("42f85136-db7e-439c-85f1-e4075d135fc8")]
		[ComImport]
		private interface IFileOpenDialog
		{
			[PreserveSig]
			uint Show([In] IntPtr parent);

			void SetFileTypes();

			void SetFileTypeIndex([In] uint iFileType);

			void GetFileTypeIndex(out uint piFileType);

			void Advise();

			void Unadvise();

			void SetOptions([In] FolderBrowserDialog.Enum1 fos);

			void GetOptions(out FolderBrowserDialog.Enum1 pfos);

			void SetDefaultFolder(FolderBrowserDialog.IShellItem psi);

			void SetFolder(FolderBrowserDialog.IShellItem psi);

			void GetFolder(out FolderBrowserDialog.IShellItem ppsi);

			void GetCurrentSelection(out FolderBrowserDialog.IShellItem ppsi);

			void SetFileName([MarshalAs(UnmanagedType.LPWStr)][In] string pszName);

			void GetFileName([MarshalAs(UnmanagedType.LPWStr)] out string pszName);

			void SetTitle([MarshalAs(UnmanagedType.LPWStr)][In] string pszTitle);

			void SetOkButtonLabel([MarshalAs(UnmanagedType.LPWStr)][In] string pszText);

			void SetFileNameLabel([MarshalAs(UnmanagedType.LPWStr)][In] string pszLabel);

			void GetResult(out FolderBrowserDialog.IShellItem ppsi);

			void AddPlace(FolderBrowserDialog.IShellItem psi, int alignment);

			void SetDefaultExtension([MarshalAs(UnmanagedType.LPWStr)][In] string pszDefaultExtension);

			void Close(int hr);

			void SetClientGuid();

			void ClearClientData();

			void SetFilter([MarshalAs(UnmanagedType.Interface)] IntPtr pFilter);

			void GetResults([MarshalAs(UnmanagedType.Interface)] out IntPtr ppenum);

			void GetSelectedItems([MarshalAs(UnmanagedType.Interface)] out IntPtr ppsai);
		}

		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("43826D1E-E718-42EE-BC55-A1E261C37BFE")]
		[ComImport]
		private interface IShellItem
		{
			void BindToHandler();

			void GetParent();

			void GetDisplayName([In] FolderBrowserDialog.Enum0 sigdnName, [MarshalAs(UnmanagedType.LPWStr)] out string ppszName);

			void GetAttributes();

			void Compare();
		}

		private enum Enum0 : uint
		{

		}

		[Flags]
		private enum Enum1
		{

		}
	}
	public class FolderNameEditor : UITypeEditor
	{
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			if (value != null)
			{
				folderBrowserDialog.SelectedPath = string.Format("{0}", value);
			}
			object result;
			if (folderBrowserDialog.ShowDialog(null) == DialogResult.OK)
			{
				result = folderBrowserDialog.SelectedPath;
			}
			else
			{
				result = value;
			}
			return result;
		}

		public FolderNameEditor()
		{

		}
	}
}
