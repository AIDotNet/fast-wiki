declare class HttpClient {
    GetStringAsync(url: string): Promise<string>;

    GetJsonAsync<T>(url: string): Promise<T>;
}

declare var HttpClient: HttpClient