package com.appsprizereactnative

import org.json.JSONArray
import org.json.JSONObject

internal fun jsonStringToMap(jsonString: String): Map<String, Any?>? {
  val map = try {
    JSONObject(jsonString).toMap()["raw"] as? Map<String, Any?>?
  } catch (exc: Exception) {
    exc.printStackTrace()
    null
  }
  return map
}

internal fun mapToJsonString(map: Map<String, Any?>?): String? {
    return try {
        map?.let { JSONObject(it).toString() }
    } catch (e: Exception) {
        e.printStackTrace()
        null
    }
}
private fun JSONObject.toMap(): Map<String, Any?> = keys().asSequence().associateWith {
  when (val value = this[it])
  {
    is JSONArray -> {
      val map = (0 until value.length()).associate { Pair(it.toString(), value[it]) }
      JSONObject(map).toMap().values.toList()
    }
    is JSONObject -> value.toMap()
    JSONObject.NULL -> null
    else            -> value
  }
}
