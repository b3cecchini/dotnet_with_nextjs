import { useParamsStore } from '@/hooks/useParamsStore';
import { Button, ButtonGroup } from 'flowbite-react';
import React from 'react'


const pageSizeButtons = [4, 8, 12];


export default function Filters() {

    const pageSize = useParamsStore(state => state.pageSize)
    const setParams = useParamsStore(state => state.setParams)

    return (
        <div
        className='jusstify-between items-center mb-4'>
            <div>
                <span className="uppercase text-sm text-gray-200 mr-2">
                    Page Size
                </span>
                <ButtonGroup>
                    {pageSizeButtons.map((value, i) => (
                        <Button key={i} onClick={() => setParams({pageSize: value})}
                        color={`${pageSize === value ? 'red' : 'gray'}`}
                        className="focus:ring-0">
                            {value}
                        </Button>
                    ))}
                </ButtonGroup>
            </div>
        </div>
    )
}
