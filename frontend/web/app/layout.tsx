import type { Metadata } from 'next'
import { Inter } from 'next/font/google'
import './globals.css'
import NavBar from './nav/NavBar'

const inter = Inter({ subsets: ['latin'] })

export const metadata: Metadata = {
  title: 'Car Auction',
  description: 'Auction application',
}

export default function RootLayout({
  children,
}: {
  children: React.ReactNode
}) {
  return (
    <html lang="en">
      <body>
        <NavBar />
        <div className=' bg-slate-600 '>
        <main className="px-8 py-5 container mx-auto">
          {children}
        </main>
        </div>
        </body>
    </html>
  )
}
