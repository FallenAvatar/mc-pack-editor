using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MCPackEditor.App.Data.Converters {
	public class SingleOrList<T> : JsonConverter {
		public override bool CanConvert( Type objectType ) {
			return (objectType == typeof( List<T> ) || objectType == typeof( IList<T> ));
		}

		public override object ReadJson( JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer ) {
			var token = JToken.Load( reader );

			if( token.Type == JTokenType.Array )
				return token.ToObject<IList<T>>() ?? new List<T>();

			var o = token.ToObject<T>();
			if( o is null )
				return new List<T>();

			return new List<T> { o };
		}



		public override void WriteJson( JsonWriter writer, object? value, JsonSerializer serializer ) {
			var list = value as IList<T>;
			if( list?.Count == 1 )
				value = list[0];

			serializer.Serialize( writer, value );
		}
	}
}
