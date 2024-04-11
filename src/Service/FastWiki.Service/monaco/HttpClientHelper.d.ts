
/**
 * 提供了一组用于发送HTTP请求的方法。
 */
declare namespace HttpClientHelper {
    /**
     * 发送一个GET请求到指定的URI，并将响应作为字符串返回。
     * @param uri 发送GET请求的URI。
     * @returns 一个解析为响应字符串的Promise。
     */
    function GetAsync(uri: string): Promise<string>;

    /**
     * 发送一个POST请求到指定的URI，并将提供的数据作为字符串返回。
     * @param uri 发送POST请求的URI。
     * @param data 要发送的数据。
     * @returns 一个解析为响应字符串的Promise。
     */
    function PostAsync(uri: string, data: object): Promise<string>;

    /**
     * 发送一个PUT请求到指定的URI，并将提供的数据作为字符串返回。
     * @param uri 发送PUT请求的URI。
     * @param data 要发送的数据。
     * @returns 一个解析为响应字符串的Promise。
     */
    function PutAsync(uri: string, data: object): Promise<string>;

    /**
     * 发送一个DELETE请求到指定的URI，并将响应作为字符串返回。
     * @param uri 发送DELETE请求的URI。
     * @returns 一个解析为响应字符串的Promise。
     */
    function DeleteAsync(uri: string): Promise<string>;
}