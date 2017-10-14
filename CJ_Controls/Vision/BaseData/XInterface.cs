using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CJ_Controls.Vision.BaseData
{
	public class XConstructor
	{
		private Assembly m_Assembly;
		private Type m_Type;
		private object m_Object;

		public XConstructor(string sPath, string sClass)
		{
			//외부 어셈블리일 경우에는 어셈블리로 부터 타입을 얻어야 한다.
			//Assembly.LoadFrom(assemblyFullPath).GetType("네임스페이스.클래스");
			//만일 현재 실행되는 어셈블리에 포함된 타입을 얻을 경우에는 아래처럼만 하면 된다
			//Type type = Type.GetType(className) 
			try
			{
				m_Assembly = Assembly.LoadFrom(sPath);
				m_Type = m_Assembly.GetType(sClass);
				m_Object = Activator.CreateInstance(m_Type);
			}
			catch (Exception pe)
			{
				string sMsg = pe.ToString();
			}
			//Default 생성자를 이용하여 동적 인스턴스 생성
			//만일 매개변수가 있는 생성자를 호출할 경우에는 object 배열을 파라메타로 전달
			//Activator.CreateInstance(type,new object[]{매개변수1,매개변수2})
			//this.obj = Activator.CreateInstance(type);			
		}


		public Type[] GetClassInfos()
		{
			Type[] types = m_Assembly.GetTypes();
			return types;
		}

		public ConstructorInfo[] GetConstructorInfos(string className)
		{
			Type type = m_Assembly.GetType(className);

			System.Reflection.ConstructorInfo[] constructorInfos =
				type.GetConstructors();
			return constructorInfos;
		}

		public MethodInfo[] GetMethodInfos(string className)
		{
			Type type = m_Assembly.GetType(className);
			System.Reflection.MethodInfo[] methodInfos =
				type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			return methodInfos;
		}

		public FieldInfo[] GetFieldInfos(string className)
		{
			Type type = m_Assembly.GetType(className);
			System.Reflection.FieldInfo[] fieldInfos =
				type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

			return fieldInfos;
		}

		public MethodInfo CreteMethod(string methodName)
		{
			return m_Type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod);
		}

		public PropertyInfo CreteProperty(string popertyName)
		{
			return m_Type.GetProperty(popertyName, BindingFlags.Public | BindingFlags.Instance);
		}

		public object GetFieldValue(string fieldName)
		{
			FieldInfo f = m_Type.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			return f.GetValue(m_Object);
		}

		public void SetFieldValue(string fieldName, object oData)
		{
			FieldInfo f = m_Type.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			f.SetValue(m_Object, oData);
		}
	}

	public class XInterface : X
	{
		private object m_Object;
		private MethodInfo m_Method;
		private PropertyInfo m_Property;
		private object[] m_Index;

		public XInterface(object _Object, PropertyInfo _Property, object[] _Index)
		{
			m_Index = _Index;
			m_Object = _Object;
			m_Property = _Property;
		}

		public object XValue
		{
			get
			{
				return m_Property.GetValue(m_Object, m_Index);
			}
			set
			{
				m_Property.SetValue(m_Object, value, m_Index);
			}
		}

		public XInterface(object _Object, MethodInfo _Method, object[] _Index)
		{
			m_Index = _Index;
			m_Object = _Object;
			m_Method = _Method;
		}

		public object Invoke()
		{
			return m_Method.Invoke(m_Object, m_Index);
		}
	}
}
