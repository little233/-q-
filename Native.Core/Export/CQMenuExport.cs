/*
 * 此文件由T4引擎自动生成, 请勿修改此文件中的代码!
 */
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Native.Core.Domain;
using Native.Sdk.Cqp;
using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp.Interface;
using Unity;

namespace Native.App.Export
{
	/// <summary>	
	/// 表示酷Q菜单导出的类	
	/// </summary>	
	public class CQMenuExport	
	{	
		#region --构造函数--	
		/// <summary>	
		/// 由托管环境初始化的 <see cref="CQMenuExport"/> 的新实例	
		/// </summary>	
		static CQMenuExport ()	
		{	
			
			// 调用方法进行实例化	
			ResolveBackcall ();	
		}	
		#endregion	
		
		#region --私有方法--	
		/// <summary>	
		/// 读取容器中的注册项, 进行事件分发	
		/// </summary>	
		private static void ResolveBackcall ()	
		{	
			/*	
			 * Name: 应用设置	
			 * Function: menuWindow	
			 */	
			if (AppData.UnityContainer.IsRegistered<IMenuCall> ("应用设置"))	
			{	
				MenumenuWindowHandler += AppData.UnityContainer.Resolve<IMenuCall> ("应用设置").MenuCall;	
			}	
			
		}	
		#endregion	
		
		#region --导出方法--	
		/*	
		 * Name: 应用设置	
		 * Function: menuWindow	
		 */	
		public static event EventHandler<CQMenuCallEventArgs> MenumenuWindowHandler;	
		[DllExport (ExportName = "menuWindow", CallingConvention = CallingConvention.StdCall)]	
		public static int MenumenuWindow ()	
		{	
			if (MenumenuWindowHandler != null)	
			{	
				CQMenuCallEventArgs args = new CQMenuCallEventArgs (AppData.CQApi, AppData.CQLog, "应用设置", "menuWindow");	
				MenumenuWindowHandler (typeof (CQMenuExport), args);	
			}	
			return 0;	
		}	
		
		#endregion	
	}	
}
