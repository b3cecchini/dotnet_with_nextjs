import React from 'react'
import { getSession, getTokenWorkaround } from '../Actions/AuthActions'
import Heading from '../components/Heading'
import AuthTest from './AuthTest'

export default async function Session() {
    const session = await getSession()
    const token = await getTokenWorkaround()
    return (
        <>
        <Heading title='Session Dashboard'/>
        <div className="bg-blue-300 border-2 border-blue-700">
            <h3 className="text-lg">Session Data</h3>
            <pre>{JSON.stringify(session, null, 2)}</pre>
        </div>
        <div className='mt-4'>
            <AuthTest />
        </div>
        <div className="mt-4 bg-green-300 border-2 border-green-700">
            <h3 className="text-lg">Session Data</h3>
            <pre className='overflow-auto'>{JSON.stringify(token, null, 2)}</pre>
        </div>
        </>
    )
}
