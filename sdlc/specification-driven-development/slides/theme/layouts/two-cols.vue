<script setup lang="ts">
const props = defineProps<{
  class?: string
  layoutClass?: string
}>()
</script>

<template>
  <div
    class="slidev-layout two-columns grid grid-cols-2"
    :class="[props.class, props.layoutClass]"
  >
    <div class="col-left">
      <slot />
      <slot name="left" />
    </div>
    <div class="col-right">
      <slot name="right" />
    </div>
  </div>
</template>
