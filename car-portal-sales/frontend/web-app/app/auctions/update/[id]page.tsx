import React from 'react'

export default function Update({params} : {params: {id: string}}) {
  return (
    <div>
      Update for auction {params.id}
    </div>
  )
}