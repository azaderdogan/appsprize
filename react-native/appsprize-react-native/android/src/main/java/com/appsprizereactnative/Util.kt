package com.appsprizereactnative

import android.annotation.SuppressLint
import android.content.Context
import android.graphics.Typeface
import android.graphics.drawable.Drawable
import androidx.core.content.ContextCompat
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

internal fun getTypeface(context: Context, fontName: String?): Typeface? {
    fontName ?: return null
    return try {
        Typeface.createFromAsset(context.assets, fontName)
    } catch (_: Exception) {
        null
    }
}

@SuppressLint("DiscouragedApi")
internal fun getDrawable(context: Context, name: String?): Drawable? {
    name ?: return null
    return try {
        val id = context.resources.getIdentifier(name, "drawable", context.packageName)
        ContextCompat.getDrawable(context, id)
    } catch (_: Exception) {
        null
     }
}
