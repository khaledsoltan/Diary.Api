export class RequestResponse {
    constructor(
        public Success?: boolean,
        public Message?: string,
        public Result?: JSON
    ) { }
}