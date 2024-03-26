export type Attributes = Record<string, string | boolean | undefined>

function classNames (elemCls: string, attributes: Attributes = {}, extra: Array<string | undefined> = []): string {
    return [
        elemCls,
        ...extra.filter(Boolean),
        ...Object.keys(attributes).filter((attr) => !!attributes[attr])
    ].join(' ')
}

export { classNames as cn }
