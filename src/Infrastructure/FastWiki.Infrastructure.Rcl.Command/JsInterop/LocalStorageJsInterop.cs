using Microsoft.JSInterop;
using System.Text.Json;

namespace FastWiki.Infrastructure.Rcl.Command.JsInterop;

/// <summary>
/// LocalStorage js封装
/// </summary>
public sealed class LocalStorageJsInterop : JSModule
{
    /// <inheritdoc />
    public LocalStorageJsInterop(IJSRuntime js) : base(js, PrefixPath + "localStorage.js")
    {
    }

    /// <summary>
    /// 设置LocalStorage的值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public async ValueTask SetLocalStorageAsync<T>(string key, T value) where T : class
        => await SetLocalStorageAsync(key, JsonSerializer.Serialize(value));

    /// <summary>
    /// 获取LocalStorage的值
    /// </summary>
    /// <typeparam name="T">指定class类型</typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    public async ValueTask<T?> GetLocalStorageAsync<T>(string key) where T : class
        => JsonSerializer.Deserialize<T>(await GetLocalStorageAsync(key));

    /// <summary>
    /// 设置LocalStorage的值
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public async ValueTask SetLocalStorageAsync(string key, string value)
        => await InvokeVoidAsync("setLocalStorage", key, value);

    /// <summary>
    /// 获取LocalStorage的值
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public async ValueTask<string> GetLocalStorageAsync(string key)
        => await InvokeAsync<string>("getLocalStorage", key);

    /// <summary>
    /// 删除指定Key的LocalStorage
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public async ValueTask RemoveLocalStorageAsync(string key)
        => await InvokeVoidAsync("removeLocalStorage", key);

    /// <summary>
    /// 批量删除Key的LocalStorage
    /// </summary>
    /// <param name="keys"></param>
    /// <returns></returns>
    public async ValueTask RemovesLocalStorageAsync(string[] keys)
        => await InvokeVoidAsync("removesLocalStorage", keys);

    /// <summary>
    /// 清空LocalStorage
    /// </summary>
    /// <returns></returns>
    public async ValueTask ClearLocalStorageAsync()
        => await InvokeVoidAsync("clearLocalStorage");

    /// <summary>
    /// 检查是否支持 localStorage
    /// </summary>
    /// <returns>返回是否支持 localStorage</returns>
    public async ValueTask<bool> IsLocalStorageSupportedAsync()
        => await InvokeAsync<bool>("isLocalStorageSupported");

    /// <summary>
    /// 获取所有LocalStorage的Key
    /// </summary>
    /// <returns></returns>
    public async ValueTask<string[]> GetLocalStorageKeysAsync()
        => await InvokeAsync<string[]>("getLocalStorageKeys");

    /// <summary>
    /// 判断sessionStorage中是否存在某个key
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public async ValueTask<bool> ContainKeyAsync(string key)
        => await InvokeAsync<bool>("containKey", key);
}