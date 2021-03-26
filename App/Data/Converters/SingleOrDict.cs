using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MCPackEditor.App.Data.Converters {
	public class SingleOrDict<T> : JsonConverter {
		public override bool CanConvert( Type objectType ) {
			return (objectType == typeof( Dictionary<string, T> ) || objectType == typeof( IDictionary<string, T> ));
		}

		public override object ReadJson( JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer ) {
			var token = JToken.Load( reader );

			if( token.Type == JTokenType.Object ) {
				var to = token.ToObject<IDictionary<string, T>>() ?? new Dictionary<string, T>();
				return to;
			}

			var o = token.ToObject<T>();
			if( o is null )
				return new Dictionary<string, T>();

			return new Dictionary<string, T> { { "", o } };
		}



		public override void WriteJson( JsonWriter writer, object? value, JsonSerializer serializer ) {
			var dict = value as IDictionary<string, T>;
			if( dict?.Count == 1 && (dict?.ContainsKey( "" ) ?? false) )
				value = dict[""];

			serializer.Serialize( writer, value );
		}
	}
}
