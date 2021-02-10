using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Bunit
{
	/// <summary>
	/// Exception use to indicate that an invocation was
	/// received by the <see cref="BunitJSInterop"/> running in <see cref="JSRuntimeMode.Strict"/> mode,
	/// which didn't contain a matching invocation handler.
	/// </summary>
	[Serializable]
	public sealed class JSRuntimeUnhandledInvocationException : Exception
	{
		/// <summary>
		/// Gets the unplanned invocation.
		/// </summary>
		public JSRuntimeInvocation Invocation { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="JSRuntimeUnhandledInvocationException"/> class
		/// with the provided <see cref="Invocation"/> attached.
		/// </summary>
		/// <param name="invocation">The unplanned invocation.</param>
		public JSRuntimeUnhandledInvocationException(JSRuntimeInvocation invocation)
			: base($"The invocation of '{invocation.Identifier}' {PrintArguments(invocation.Arguments)} was not expected.")
		{
			Invocation = invocation;
		}

		private static string PrintArguments(IReadOnlyList<object?> arguments)
		{
			if (arguments.Count == 0)
				return "without arguments";
			if (arguments.Count == 1)
				return $"with the argument [{arguments[0]}]";

			return $"with arguments [{string.Join(", ", arguments.OfType<object>().Select(x => x.ToString()))}]";
		}

		private JSRuntimeUnhandledInvocationException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}
	}
}
