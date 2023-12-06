'use client'

import React, { useEffect, useState } from 'react'
import AuctionCard from './AuctionCard'
import { PagedResult, Auction } from '../types'
import AppPagination from '../components/AppPagination'
import { GetData } from '../Actions/AuctionActions'
import Filters from './Filters'
import { useParamsStore } from '@/hooks/useParamsStore'
import { shallow } from 'zustand/shallow'
import qs from 'query-string'
import EmptyFilter from '../components/EmptyFilter'


export default function Listings() {
    const [data, setData] = useState<PagedResult<Auction>>()
    const params = useParamsStore(state => ({
      pageNumber: state.pageNumber,
      pageSize: state.pageSize,
      searchTerms: state.searchTerms
    }), shallow)

    const setParams = useParamsStore(state => state.setParams)
    const url = qs.stringify(params)

    function setPageNumber(pageNumber: number) {
      setParams({pageNumber: pageNumber})
    }

    useEffect(() => {
      GetData(url).then( data => {
        setData(data)
      })
    }, [url])
    
  if (!data) return <h3> Loading... </h3> 

  if (data.totalCount == 0) return <EmptyFilter showReset/>
  return (
    <>
    <Filters />
      <div className="grid grid-cols-4 gap-6">
        {data.results.map( (auction: any) => (
          <AuctionCard key={auction.id} auction={auction} />
        ))}
      </div>
      <div className='flex justify-center mt-4'>
        <AppPagination pageChanged={setPageNumber} currentPage={params.pageNumber} pageCount={data.pageCount}/>
      </div>
    </>
  )
}
