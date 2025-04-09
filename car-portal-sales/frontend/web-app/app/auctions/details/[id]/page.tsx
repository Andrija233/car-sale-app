import React from 'react'

export default function Details({params} : {params: {id: string}}) {
  return (
    <div>
      Details for auction {params.id}
    </div>
  )
}
