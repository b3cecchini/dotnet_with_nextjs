'use client'

import { useParamsStore } from '@/hooks/useParamsStore'
import React, { useState } from 'react'
import {FaSearch} from 'react-icons/fa'

export default function Search() {
    const setParms = useParamsStore(state => state.setParams)
    const setSearchValue = useParamsStore(state => state.setSearchValue)
    const searchValue = useParamsStore(state => state.searchValue)
    
    function changeSearchValue(event: any) {
        setSearchValue(event.target.value)
    }

    function search() {
        setParms({searchTerms: searchValue})
    }
    return (
        <div className="flex w-[50%] items-center border-2 rounded-full py-2 shadow-sm">
            <input 
            type="text" 
            value={searchValue}
            placeholder="Search by make, model, or color"
            className="flex-grow padding-left-5 bg-transparent focus:outline-none focus:border-none focus:ring-0 text-sm text-gray-900 "
            onChange={changeSearchValue}
            onKeyDown={(e: any) => {if (e.key == "Enter") search()}}/>
            <button onClick={search}>
                <FaSearch size={34} className="bg-red-400 text-gray-200 rounded-full p-2 cursor-pointer mx-2"/>
            </button>
        </div>
    )
}
