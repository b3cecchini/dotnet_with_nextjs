'use server'
import { Auction, PagedResult } from "../types"

export async function GetData(query: string): Promise<PagedResult<Auction>> {
    const res = await fetch(`http://localhost:6001/search${query}` )

    if(!res.ok)
    {
        throw new Error("Unable to fetch data")
    }

    return res.json()
}